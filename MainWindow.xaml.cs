using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskTrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TaskObjectLogic.LoadList();
            lb_Tasks.ItemsSource = TaskObjectLogic.toDo;
            lb_Tasks.DataContext = TaskObjectLogic.toDo;
            btn_SwitchTasks.Content = "Show Completed Tasks";
            txtblk_ActiveOrComplete.Text = "Active Tasks:";
            DataContext = this;
        }


        string taskNameArgument;
        private void taskNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            taskNameArgument = taskNameInput.Text;
        }


        string taskDescriptionArgument;
        private void taskDescriptionInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            taskDescriptionArgument = taskDescriptionInput.Text;
        }

        
        private void DisplaySelectedTask(TaskObject taskObject)
        {
            txtbx_TaskNameDisplay.Text = taskObject.taskName;
            txtbx_TaskDescriptionDisplay.Text = taskObject.taskDescription;
        }



        private void btn_SwitchTasks_Click(object sender, RoutedEventArgs e)
        {
            if (btn_SwitchTasks.Content.ToString() == "Show Active Tasks")
            {
                lb_Tasks.ItemsSource = TaskObjectLogic.toDo;
                lb_Tasks.DataContext = TaskObjectLogic.toDo;
                btn_SwitchTasks.Content = "Show Completed Tasks";
                txtblk_ActiveOrComplete.Text = "Active Tasks:";
                return;
            }
            if (btn_SwitchTasks.Content.ToString() == "Show Completed Tasks")
            {
                lb_Tasks.ItemsSource = TaskObjectLogic.completedTasks;
                lb_Tasks.DataContext = TaskObjectLogic.completedTasks;
                btn_SwitchTasks.Content = "Show Active Tasks";
                txtblk_ActiveOrComplete.Text = "Completed Tasks:";
                return;
            }
        }


        private void btn_CreateTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskNameArgument != null & taskDescriptionArgument != null)
            {
                TaskObjectLogic.CreateTask(taskNameArgument, taskDescriptionArgument);
                lb_Tasks.ItemsSource = TaskObjectLogic.toDo;
                lb_Tasks.DataContext = TaskObjectLogic.toDo;
                btn_SwitchTasks.Content = "Show Completed Tasks";
                txtblk_ActiveOrComplete.Text = "Active Tasks:";
                taskNameInput.Text = "";
                taskDescriptionInput.Text = "";
            }
        }


        private void lb_Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_Tasks.SelectedItem != null)
            {
                if (lb_Tasks.SelectedItem is TaskObject taskObject)
                {
                    DisplaySelectedTask(taskObject);
                    if (taskObject.completed == true)
                    {
                        btn_MarkAsCompleted.Content = "Mark As Active";
                        return;
                    }
                    if (taskObject.completed == false)
                    {
                        btn_MarkAsCompleted.Content = "Mark As Completed";
                        return;
                    }
                }
            }
        }


        private void btn_MarkAsCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (lb_Tasks.SelectedItem is TaskObject taskObject)
            {
                if (taskObject.completed == false)
                {
                    taskObject.completed = true;
                    TaskObjectLogic.Update();
                }
                if (taskObject.completed == true)
                {
                    taskObject.completed = false;
                    TaskObjectLogic.Update();
                }
                txtbx_TaskNameDisplay.Text = "";
                txtbx_TaskDescriptionDisplay.Text = "";
            }
        }

        private void btn_DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lb_Tasks.SelectedItem is TaskObject taskObject)
            {
                if (TaskObjectLogic.toDo.Contains(taskObject))
                {
                    TaskObjectLogic.toDo.Remove(taskObject);
                }
                if (TaskObjectLogic.completedTasks.Contains(taskObject))
                {
                    TaskObjectLogic.completedTasks.Remove(taskObject);
                }
                TaskObjectLogic.Update();
                txtbx_TaskNameDisplay.Text = "";
                txtbx_TaskDescriptionDisplay.Text = "";
            }
        }
    }
}



//Task objects not showing up in box atm, idk y