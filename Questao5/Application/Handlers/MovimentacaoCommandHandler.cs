using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure;
using static Questao5.Domain.Enumerators.Enumerators;
using Questao5.Infrastructure.Database.QueryStore;
using Questao5.Domain.Shared;

namespace Questao5.Application.Handlers
{
    public class MovimentacaoCommandHandler : IRequestHandler<MovimentacaoCommand, string>
    {
        private readonly ICommandStore<Movimento> _movimentoCommandStore;
        private readonly ICommandStore<IdEmPotencia> _idEmPotenciaCommandStore;
        private readonly IQueryStore<ContaCorrente, string> _contaCorrenteQueryStore;
        private readonly IQueryStore<IdEmPotencia, string> _idEmPotenciaQueryStore;
        private readonly IUnitOfWork _unitOfWork;

        public MovimentacaoCommandHandler(ICommandStore<Movimento> movimentoCommandStore,
                                          ICommandStore<IdEmPotencia> idEmPotenciaCommandStore,
                                          IQueryStore<ContaCorrente, string> contaCorrenteQueryStore,
                                          IQueryStore<IdEmPotencia, string> idEmPotenciaQueryStore,
                                          IUnitOfWork unitOfWork)
        {
            this._movimentoCommandStore = movimentoCommandStore;
            this._idEmPotenciaCommandStore = idEmPotenciaCommandStore;
            this._contaCorrenteQueryStore = contaCorrenteQueryStore;
            this._idEmPotenciaQueryStore = idEmPotenciaQueryStore;
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(MovimentacaoCommand request, CancellationToken cancellationToken)
        {
            var movimento = new Movimento();

            try
            {
                _unitOfWork.BeginTransaction();
                
                movimento = await AddMovimento(request);

                _unitOfWork.Commit();
            }
            catch (CustomException ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
            
            return movimento.IdMovimento;
        }

        private async Task<Movimento> AddMovimento(MovimentacaoCommand request)
        {
            var movimento = new Movimento
            {
                DataMovimento = DateTime.Now.ToString("dd/MM/yyyy"),
                IdContaCorrente = request.IdContaCorrente.ToString().ToUpper(),
                TipoMovimento = request.TipoMovimento,
                IdMovimento = Guid.NewGuid().ToString().ToUpper(),
                Valor = request.Valor
            };

            var idEmPotencia = await _idEmPotenciaQueryStore.Get(request.IdRequisicao.ToString().ToUpper());

            if (idEmPotencia == null)
            {
                await ValidaMovimentacao(request);

                await _movimentoCommandStore.Add(movimento);

                await AddIdEmPotencia(request, movimento);

                return movimento;
            }
            else
            {
                movimento.IdMovimento = idEmPotencia.resultado;
                return movimento;
            }            
        }

        private async Task AddIdEmPotencia(MovimentacaoCommand request, Movimento movimento)
        {
            var idEmPotencia = new IdEmPotencia
            {
                chave_idempotencia = request.IdRequisicao.ToString().ToUpper(),
                requisicao = JsonConvert.SerializeObject(request),
                resultado = movimento.IdMovimento
            };

            await _idEmPotenciaCommandStore.Add(idEmPotencia);
        }

        private async Task ValidaMovimentacao(MovimentacaoCommand request)
        {
            var contaCorrente = await _contaCorrenteQueryStore.Get(request.IdContaCorrente.ToString().ToUpper());

            Utils.VerifyContaCorrente(contaCorrente);

            VerifyRequestMovimentacao(request);
        }

        private void VerifyRequestMovimentacao(MovimentacaoCommand request)
        {
            if (request.Valor < 0)
            {
                throw new CustomException(ENUM_CUSTOM_ERROR.INVALID_VALUE);
            }

            if (!(request.TipoMovimento == "C" || request.TipoMovimento == "D"))
            {
                throw new CustomException(ENUM_CUSTOM_ERROR.INVALID_TYPE);
            }
        }
    }
}
