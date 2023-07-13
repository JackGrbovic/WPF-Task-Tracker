using System.Xml;
using Newtonsoft.Json;

namespace TaskTrackerWPF
{
    public class TaskObject
    {
        private int ID;
        private string TaskName;
        private string TaskDescription;
        private string DateSet;
        private string DateCompleted;
        private bool? Completed;

        public int iD { get; set; }
        public string taskName { get; set; }
        public string taskDescription { get; set; }
        public string dateSet { get; set; }
        public string dateCompleted { get; set; }
        public bool? completed { get; set; }

        public TaskObject(string pTaskName, string pTaskDescription)
        {
            taskName = pTaskName;
            taskDescription = pTaskDescription;

            ID = iD;
            TaskName = taskName;
            TaskDescription = taskDescription;
            DateSet = dateSet;
            DateCompleted = dateCompleted;
            Completed = completed;
        }
    }
}
