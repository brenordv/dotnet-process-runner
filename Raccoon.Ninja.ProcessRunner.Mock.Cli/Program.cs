/* Simple Console Application that finds prime numbers and prints it.
 *
 * The purpose of this app is just to serve as a mock/test app for ProcessRunner.
 */

var name = args.Length == 0 ? "Undefined" : args[0];
var pid = Environment.ProcessId;
var logName = $"[{pid}] {name}";
const int numbersPerLine = 5;
var n = 1;
var primeInLine = 0;

Console.Write($"{logName}:");
while (true)
{
    Thread.Sleep(5);
    var a = 0;
    for (var i = 1; i <= n; i++)
    {
        if (n % i != 0) continue;
        a++;
    }

    if (primeInLine > 0 && primeInLine % numbersPerLine == 0)
    {
        Console.Write($"\n{logName}:");
        primeInLine = 0;
    }

    if (a == 2)
    {
        primeInLine++;
        Console.Write($" {n:000000}");
    }

    n++;
}