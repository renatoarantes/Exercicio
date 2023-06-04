using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int Numero { get; }
        public string Titular { get; }
        public double Saldo { get; set; }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }

        internal void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        internal void Saque(double quantia)
        {
            Saldo = Saldo - quantia - 3.50;
        }

        public override string ToString()
        {
            return string.Format("Conta {0}, Titular: {1}, Saldo: $ {2:0.00}", Numero, Titular, Saldo);
        }
    }
}
