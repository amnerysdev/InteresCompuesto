using System.Globalization;
using System.Text;

namespace InteresCompuesto;

/// <summary>
/// Módulo de Capitalización Recursiva.
/// Calcula el monto final de una inversión con interés compuesto anual,
/// usando únicamente recursividad (sin bucles for ni while).
/// </summary>
internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== Interés compuesto (recursivo) ===");
        Console.WriteLine();

        decimal capital = LeerDecimal("Capital inicial (m): ", minimo: 0m);
        decimal tasa    = LeerDecimal("Tasa de interés anual en % (X): ", minimo: 0m);
        int anios       = LeerEntero("Tiempo en años (n): ", minimo: 0);

        // Criterio 2: la tasa se convierte internamente a su equivalente decimal.
        decimal i = tasa / 100m;

        decimal monto = CalcularMonto(capital, i, anios);

        Console.WriteLine();
        Console.WriteLine($"Monto total acumulado después de {anios} año(s): {monto.ToString("N2", CultureInfo.InvariantCulture)}");
    }

    /// <summary>
    /// Calcula C(n), el monto acumulado después de n años.
    /// </summary>
    /// <param name="capital">Capital inicial (m).</param>
    /// <param name="i">Tasa en factor decimal (X / 100).</param>
    /// <param name="n">Años transcurridos.</param>
    /// <returns>Monto acumulado C(n).</returns>
    private static decimal CalcularMonto(decimal capital, decimal i, int n)
    {
        // Caso base: C(0) = m
        if (n == 0)
        {
            return capital;
        }

        // Caso recursivo: C(n) = C(n - 1) * (1 + i)
        return CalcularMonto(capital, i, n - 1) * (1m + i);
    }

    // ---------- Lectura de datos (también recursiva, para no usar bucles) ----------

    /// <summary>Pide un número decimal y vuelve a preguntar si el dato no es válido.</summary>
    private static decimal LeerDecimal(string mensaje, decimal minimo)
    {
        Console.Write(mensaje);
        string texto = (Console.ReadLine() ?? string.Empty).Trim().Replace(',', '.');

        if (decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor)
            && valor >= minimo)
        {
            return valor;
        }

        Console.WriteLine($"  Valor no válido. Escribe un número mayor o igual a {minimo}.");
        return LeerDecimal(mensaje, minimo);
    }

    /// <summary>Pide un número entero y vuelve a preguntar si el dato no es válido.</summary>
    private static int LeerEntero(string mensaje, int minimo)
    {
        Console.Write(mensaje);
        string texto = (Console.ReadLine() ?? string.Empty).Trim();

        if (int.TryParse(texto, NumberStyles.Integer, CultureInfo.InvariantCulture, out int valor)
            && valor >= minimo)
        {
            return valor;
        }

        Console.WriteLine($"  Valor no válido. Escribe un número entero mayor o igual a {minimo}.");
        return LeerEntero(mensaje, minimo);
    }
}
