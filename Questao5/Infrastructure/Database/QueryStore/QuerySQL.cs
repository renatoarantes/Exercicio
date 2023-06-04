namespace Questao5.Infrastructure.Database.QueryStore
{
    public static class QuerySQL
    {
        public static string selectContaCorrente = @"SELECT    IdContaCorrente,
                                                        Numero,
                                                        Nome,
                                                        Ativo
                                                    FROM    contacorrente
                                                    WHERE   IdContaCorrente = @IdContaCorrente";

        public static string selectIdEmPotencia = @" SELECT chave_idempotencia,
                                                     requisicao,
                                                     resultado
                                                  FROM   IdEmPotencia
                                                  WHERE chave_idempotencia = @chave_idempotencia";

        public static string selectSaldoContaCorrente =
            @"
                DROP TABLE IF EXISTS somaCredito ;
                DROP TABLE IF EXISTS somaDebito ;

                CREATE TEMPORARY TABLE somaCredito as 
                SELECT ifnull(SUM(Valor), 0) somaCredito
                FROM movimento m
                where m.idcontacorrente = @IdContaCorrente
	                 and tipoMovimento= 'C';
	 
                CREATE TEMPORARY TABLE somaDebito as 
                SELECT ifnull(SUM(Valor), 0) somaDebito
                FROM movimento m
                where m.idcontacorrente = @IdContaCorrente
	                 and tipoMovimento= 'D';


                select ifnull(ifnull(c.somaCredito,0) - ifnull(d.somaDebito,0),0) 
                from somaCredito c, somaDebito d ;

            ";
    }
}
