using System;
using System.IO;
using System.Linq;

using System.Xml;
using System.Xml.Serialization;

using CircuitRunner;

public static class Construction
{
    public static Device Deserialise(string fileName)
    {
        Device device;

        // Construct an instance of the XmlSerializer with the type of object that is being deserialized.  
        XmlSerializer mySerializer = new XmlSerializer(typeof(Device));

        // To read the file, create a FileStream.  
        FileStream myFileStream = new FileStream(fileName, FileMode.Open);

        // Call the Deserialize method and cast to the object type.  
        device = (Device)mySerializer.Deserialize(myFileStream);

        CreateDeviceConnections(device);

        return device;
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

            /*
            foreach (Buffer bf in ct.Buffers)
            {
                //Console.WriteLine("Processing buffer {0}", bf.Id);
                bf.ParentId = ct.Id;

                foreach (Terminal tm in bf.Terminals)
                {
                    bf.AttachTerminal(bf, GetNodeSource(device, tm), tm.Id);
                }
            }
            */
            foreach (Gate gt in ct.Gates.ToList())
            {
                //Console.WriteLine("Processing gate {0}", gt.Id);
                gt.ParentId = ct.Id;

                foreach (Terminal tm in gt.Terminals.ToList())
                {
                    gt.AttachTerminal(gt, GetNodeSource(device, tm), tm.Id);
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
            case "Input":
                return c.Inputs.GetItem(t.SourceObject); ;
            case "Gate":
                return c.Gates.GetItem(t.SourceObject);
        }

        return null;
    }
}
