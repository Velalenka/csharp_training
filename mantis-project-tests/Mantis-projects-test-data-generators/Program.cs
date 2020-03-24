using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_project_tests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Mantis_projects_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);
            string format = args[2];

            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < count; i++)
            {
                projects.Add(new ProjectData(TestBase.GenerateRandomString(10))
                {
                });
            }
            if (format == "json")
            {
                WriteProjectsToJsonFile(projects, writer);
            }
            else
            {
                System.Console.Out.Write("Unrecognized format " + format);
            }
            writer.Close();
        }

        static void WriteProjectsToJsonFile(List<ProjectData> projects, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(projects, Newtonsoft.Json.Formatting.Indented));
        }
    }
}