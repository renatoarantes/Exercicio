namespace Questao5.Infrastructure.Database.CommandStore
{
    public static class CommandStoreSQL
    {
        public static string insertIdEmPotenciaSQL = @"Insert Into IdEmPotencia  (chave_idempotencia,
                                                                                  requisicao,
                                                                                  resultado)
                                                                     VALUES      (@chave_idempotencia,   
                                                                                  @requisicao,
                                                                                  @resultado)";
        public static string insertMovimentoSQL = @"Insert Into Movimento  (idmovimento,
                                                                            idcontacorrente,
                                                                            datamovimento,
                                                                            tipomovimento,
                                                                            valor) 
                                                                    VALUES (@IdMovimento,
                                                                            @IdContaCorrente,
                                                                            @DataMovimento,
                                                                            @TipoMovimento,
                                                                            @Valor)";
    }
}
