using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData(string name)
        {
            Name = name;
        }

        public override int GetHashCode()
        {
                return Name.GetHashCode();
        }

        public int CompareTo(ProjectData other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(ProjectData other)
        {
            return this.Name.Equals(other.Name);
        }

        public override string ToString()
        {
            return "name=" + Name;
        }

        public string Name { get; set; }
    }
}
