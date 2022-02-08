using System;
using System.IO;
using System.Linq;

using System.Xml;
using System.Xml.Serialization;

namespace CircuitRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Next up - logging.  Create log class with verbose switch - comment amended
            // Added another comment

            //var device = CreateDeviceFromXML("Configuration.xml");
            var device = Construction.Deserialise("../../../TwoNumberAdder_config.xml");

            device.SetInput("InputA1", true);
            device.SetInput("InputA2", true);

            //device.SetInput("InputB1", true);
            //device.SetInput("InputB2", true);


            Console.WriteLine("================ PRE-EVALUTION ================");

            foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(true, false);
            }

            device.SetInput("InputA2", false);
            foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(true, false);
            }

            /*
            Console.WriteLine("RESULTS: C3O2Out: {0} C4O1Out: {1} C1A2Out: {2}",
                                device.Circuits.GetItem("C3").Outputs.GetItem("C3O2Out").State,
                                device.Circuits.GetItem("C4").Outputs.GetItem("C4O1Out").State,
                                device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);

            Console.WriteLine("================ EVALUTION ================");

            foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(false, true);
            }

            Console.WriteLine("RESULTS: C3O2Out: {0} C4O1Out: {1} C1A2Out: {2}",
                                device.Circuits.GetItem("C3").Outputs.GetItem("C3O2Out").State,
                                device.Circuits.GetItem("C4").Outputs.GetItem("C4O1Out").State,
                                device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);

            Console.WriteLine("================ POST-EVALUTION ================");

            foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(true, false);
            }

            Console.WriteLine("RESULTS: C3O2Out: {0} C4O1Out: {1} C1A2Out: {2}",
                                device.Circuits.GetItem("C3").Outputs.GetItem("C3O2Out").State,
                                device.Circuits.GetItem("C4").Outputs.GetItem("C4O1Out").State,
                                device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);
            */
            //device.SetInput("InputA2", false);
            //device.SetInput("InputB2", false);

            /*foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(false, true);
            }

            Console.WriteLine("RESULTS: C3O2Out: {0} C4O1Out: {1} C1A2Out: {2}",
                                device.Circuits.GetItem("C3").Outputs.GetItem("C3O2Out").State,
                                device.Circuits.GetItem("C4").Outputs.GetItem("C4O1Out").State,
                                device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);
            */
            //Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);
            //Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);

            /*device.SetInput("InputA", false);
            device.SetInput("InputB", true);
            device.SetInput("CarryIn", false);
            Console.WriteLine("A0 B1 C0");
            Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1CarryOut").State);
            Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);

            device.SetInput("InputA", false);
            device.SetInput("InputB", true);
            device.SetInput("CarryIn", true);
            Console.WriteLine("A0 B1 C1");
            Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1CarryOut").State);
            Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);

            device.SetInput("InputA", true);
            device.SetInput("InputB", false);
            device.SetInput("CarryIn", false);
            Console.WriteLine("A1 B0 C0");
            Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1CarryOut").State);
            Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);

            device.SetInput("InputA", true);
            device.SetInput("InputB", false);
            device.SetInput("CarryIn", true);
            Console.WriteLine("A1 B0 C1");
            Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1CarryOut").State);
            Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);

            device.SetInput("InputA", true);
            device.SetInput("InputB", true);
            device.SetInput("CarryIn", false);
            Console.WriteLine("A1 B1 C0");
            Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1CarryOut").State);
            Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);

            device.SetInput("InputA", true);
            device.SetInput("InputB", true);
            device.SetInput("CarryIn", true);
            Console.WriteLine("A1 B1 C1");
            Console.WriteLine("Carry: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1CarryOut").State);
            Console.WriteLine("Sum: {0}", device.Circuits.GetItem("C1").Outputs.GetItem("C1SumOut").State);*/
        }
    }
}
