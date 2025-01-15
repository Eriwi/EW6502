// See https://aka.ms/new-console-template for more information
using EW6502;

var bus = new Bus();
var cpu = bus.CPU;

bus.Write(0xFFFC, 0x02);
bus.Write(0xFFFD, 0x01);

cpu.Reset();

var test = cpu.PC;

Console.WriteLine("done");

