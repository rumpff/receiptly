// See https://aka.ms/new-console-template for more information
using receiptly;
using System.Text;

Printer printer = new Printer("80mm Series Printer");

//printer.Queue(PrintCommands.Character.Font(0));
//printer.QueueLine($"Current date and time: {DateTime.Now}");

//printer.Queue(PrintCommands.Character.RightSideSpacing(0));
//printer.QueueLine($"Current date and time: {DateTime.Now}");
//printer.QueueLine("kjlhasdfkjhsdafhjsadf");


// printer.QueueLine("\x1d\x6b\x08\x7b\x42\x43\x6f\x64\x65\x20\x31\x32\x38\x00");

//printer.QueueLine(PrintCommands.Barcode.SetHRI(1));
//printer.QueueLine(PrintCommands.Barcode.Generate("test123test123"));

printer.QueueLine("Hello ma budda");
printer.Queue(PrintCommands.Barcode.SetHRI(2));
printer.Queue(PrintCommands.Barcode.Generate("bernard"));

printer.Queue("\n\n\n\n\n\n\n\n\n");
// printer.Queue("\x1B\x6D");
// printer.Queue("\x1d\x56\x00");

printer.Queue(PrintCommands.Mechanism.Cut());

printer.Print();