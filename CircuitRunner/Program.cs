using System;
using System.IO;
using System.Linq;

using System.Xml;
using System.Xml.Serialization;

namespace CircuitDesign_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Next up - logging.  Create log class with verbose switch

            //var device = CreateDeviceFromXML("Configuration.xml");
            var device = Deserialise("../../../TwoNumberAdder_config.xml");
            CreateDeviceConnections(device);

            device.SetInput("InputA1", true);
            device.SetInput("InputA2", false);

            device.SetInput("InputB1", true);
            device.SetInput("InputB2", true);

            /*foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(false, true);
            }*/

            Console.WriteLine("RESULTS: C3O2Out: {0} C4O1Out: {1} C1A2Out: {2}",
                                device.Circuits.GetItem("C3").Outputs.GetItem("C3O2Out").State,
                                device.Circuits.GetItem("C4").Outputs.GetItem("C4O1Out").State,
                                device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);

            device.SetInput("InputA2", false);
            device.SetInput("InputB2", false);

            /*foreach (Circuit ct in device.Circuits)
            {
                ct.Evaluate(false, true);
            }*/

            Console.WriteLine("RESULTS: C3O2Out: {0} C4O1Out: {1} C1A2Out: {2}",
                                device.Circuits.GetItem("C3").Outputs.GetItem("C3O2Out").State,
                                device.Circuits.GetItem("C4").Outputs.GetItem("C4O1Out").State,
                                device.Circuits.GetItem("C1").Outputs.GetItem("C1A2Out").State);

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

        static void CreateDeviceConnections(Device device)
        {
            foreach (Input ip in device.Inputs)
            {
                ip.ParentId = device.Id;
                //ip.RaiseEvents = false;
            }

            foreach (Circuit ct in device.Circuits)
            {
                //Console.WriteLine("Processing circuit {0}", ct.Id);
                
                foreach (Buffer bf in ct.Buffers)
                {
                    //Console.WriteLine("Processing buffer {0}", bf.Id);
                    bf.ParentId = ct.Id;

                    foreach (Terminal tm in bf.Terminals)
                    {
                        bf.AttachTerminal(bf, GetNodeSource(device, tm), tm.Id);
                    }
                }

                foreach (Gate gt in ct.Gates.ToList())
                {
                    //Console.WriteLine("Processing gate {0}", gt.Id);
                    gt.ParentId = ct.Id;

                    foreach (Terminal tm in gt.Terminals.ToList())
                    {
                        gt.AttachTerminal(gt, GetNodeSource(ct, tm), tm.Id);
                    }
                }

                foreach (Output op in ct.Outputs.ToList())
                {
                    //Console.WriteLine("Processing output {0}", op.Id);
                    op.ParentId = ct.Id;

                    foreach (Terminal tm in op.Terminals.ToList())
                    {
                        op.AttachTerminal(op, GetNodeSource(ct, tm), tm.Id);
                    }
                }
            }
        }

        static Node GetNodeSource(Device d, Terminal t)
        {
            switch (t.SourceType)
            {
                case "CircuitOutput":
                    return d.Circuits.GetItem(t.SourceParent).Outputs.GetItem(t.SourceObject);
                case "DeviceInput":
                    return d.Inputs.GetItem(t.SourceObject);
            }

            return null;
        }

        static Node GetNodeSource(Circuit c, Terminal t)
        {
            switch (t.SourceType)
            {
                case "Buffer":
                    return c.Buffers.GetItem(t.SourceObject);;
                case "Gate":
                    return c.Gates.GetItem(t.SourceObject);
            }

            return null;
        }

        static Device Deserialise(string fileName)
        {
            Device device;
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer =
            new XmlSerializer(typeof(Device));
            // To read the file, create a FileStream.  
            FileStream myFileStream =
            new FileStream(fileName, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            device = (Device)mySerializer.Deserialize(myFileStream);
            return device;
        }
    }
}
