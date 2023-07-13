using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace TaskTrackerWPF
{
    public static class TaskObjectLogic
    {

        static string FilePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "TaskObjects.txt";

        public static void Update()
        {
            List<ObservableCollection<TaskObject>> obsList = new List<ObservableCollection<TaskObject>>();
            obsList.Add(toDo);
            obsList.Add(completedTasks);

            File.WriteAllText(FilePath, string.Empty);
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                string listJson = JsonConvert.SerializeObject(obsList, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                sw.WriteLine(listJson);
            }
            toDo.Clear();
            completedTasks.Clear();
            LoadList();
        }


        public static void LoadList()
        {
            List<ObservableCollection<TaskObject>> obsList = new List<ObservableCollection<TaskObject>>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string fileJSON = sr.ReadToEnd();
                obsList = JsonConvert.DeserializeObject<List<ObservableCollection<TaskObject>>>(fileJSON, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            }

            foreach (ObservableCollection<TaskObject> observableCollection in obsList)
            {
                SortCollections(observableCollection);
            }
        }


        public static void CreateTask(string pTaskName, string pTaskDescription)
        {
            TaskObject taskObject = new TaskObject(pTaskName, pTaskDescription);
            taskObject.dateSet = DateTime.Now.ToShortDateString();
            taskObject.completed = false;
            toDo.Add(taskObject);

            Update();
        }


        public static ObservableCollection<TaskObject> toDo = new ObservableCollection<TaskObject>();

        public static ObservableCollection<TaskObject> completedTasks = new ObservableCollection<TaskObject>();


        public static void SortCollections(ObservableCollection<TaskObject> toSort)
        {
            foreach (TaskObject taskObject in toSort)
            {
                if (taskObject.completed == false)
                {
                    toDo.Add(taskObject);
                }
                
                if (taskObject.completed == true)
                {
                    completedTasks.Add(taskObject);
                }
            }
        }
    }
}
