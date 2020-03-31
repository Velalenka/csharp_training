using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData(string n)
        {
            name = n;
        }

        public ProjectData(Mantis.ProjectData newData)
        {
            name = newData.name;
            id = newData.id;
        }

        public ProjectData()
        {
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public int CompareTo(ProjectData other)
        {
            return this.name.CompareTo(other.name);
        }

        public bool Equals(ProjectData other)
        {
            return this.name.Equals(other.name);
        }

        public override string ToString()
        {
            return "name=" + name;
        }

        public string name { get; set; }
        public string id { get; set; }
    }
}
