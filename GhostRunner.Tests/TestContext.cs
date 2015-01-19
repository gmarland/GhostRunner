using GhostRunner.Models;
using GhostRunner.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace GhostRunner.Tests
{
    public class TestContext : IContext
    {
        public IDbSet<User> Users { get; set; }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Sequence> Sequences { get; set; }

        public IDbSet<SequenceScript> SequenceScripts { get; set; }

        public IDbSet<ScheduleDetail> ScheduleDetails { get; set; }

        public IDbSet<Script> Scripts { get; set; }

        public IDbSet<Task> Tasks { get; set; }

        public IDbSet<TaskScript> TaskScripts { get; set; }

        public IDbSet<TaskScriptParameter> TaskScriptParameters { get; set; }

        public IDbSet<Schedule> Schedules { get; set; }

        public IDbSet<ScheduleParameter> ScheduleParameters { get; set; }

        public IDbSet<PackageCache> PackageCaches { get; set; }

        public TestContext()
        {
            Users = new TestDbSet<User>();
            Projects = new TestDbSet<Project>();
            Sequences = new TestDbSet<Sequence>();
            Scripts = new TestDbSet<Script>();
            SequenceScripts = new TestDbSet<SequenceScript>();
            Tasks = new TestDbSet<Task>();
            TaskScripts = new TestDbSet<TaskScript>();
            TaskScriptParameters = new TestDbSet<TaskScriptParameter>();
            Schedules = new TestDbSet<Schedule>();
            ScheduleParameters = new TestDbSet<ScheduleParameter>();
            ScheduleDetails = new TestDbSet<ScheduleDetail>();
            PackageCaches = new TestDbSet<PackageCache>();

            BuildUsers();
            BuildProjects();
            BuildSequences();
            BuildScripts();
            BuildSequenceScripts();
            BuildTasks();
            BuildSchedules();
            BuildPackageCache();
        }

        private void BuildUsers()
        {
            User user1 = new User();
            user1.ID = 1;
            user1.ExternalId = "78597f62-553a-412b-ba0b-1ff71b1b7dae";
            user1.SessionId = "e36fbc0a-99b0-450c-83d7-9077820db7bc";
            user1.Name = "Test User 1";
            user1.Email = "test.user.1@gmail.com";
            user1.Password = EncryptionHelper.Hash("test", "SarahMcGowan");
            user1.Created = DateTime.Now;
            user1.Projects = new List<Project>();

            Users.Add(user1);

            User user2 = new User();
            user2.ID = 2;
            user2.ExternalId = "1dad8d7b-520c-4909-a3d0-a94ebf775752";
            user2.SessionId = "c91ee0d1-87fd-41fd-9db7-c865923e1c2c";
            user2.Name = "Test User 2";
            user2.Email = "test.user.2@gmail.com";
            user2.Password = EncryptionHelper.Hash("test", "SarahMcGowan");
            user2.Created = DateTime.Now;
            user2.Projects = new List<Project>();

            Users.Add(user2);

            User user3 = new User();
            user3.ID = 3;
            user3.ExternalId = "1cc3afed-c375-4ac3-a998-54f7c7a2ff3e";
            user3.SessionId = "f00026a5-3dba-453a-a9ca-12bd525477fb";
            user3.Name = "Test User 3";
            user3.Email = "test.user.3@gmail.com";
            user3.Password = EncryptionHelper.Hash("test", "SarahMcGowan");
            user3.Created = DateTime.Now;
            user3.Projects = new List<Project>();

            Users.Add(user3);
        }
    
        private void BuildProjects()
        {
            Project project1 = new Project();
            project1.ID = 1;
            project1.ExternalId = "d4708c0d-721e-426e-b49e-35990687db22";
            project1.Name = "Test Project 1";
            project1.Created = DateTime.Now;
            project1.Scripts = new List<Script>();

            Users.SingleOrDefault(u => u.ID == 1).Projects.Add(project1);
            project1.User = Users.SingleOrDefault(u => u.ID == 1);

            Projects.Add(project1);

            Project project2 = new Project();
            project2.ID = 2;
            project2.ExternalId = "bcc831de-ed6f-480b-a9dd-28de07fe7b19";
            project2.Name = "Test Project 2";
            project2.Created = DateTime.Now;
            project2.Scripts = new List<Script>();

            Users.SingleOrDefault(u => u.ID == 1).Projects.Add(project2);
            project2.User = Users.SingleOrDefault(u => u.ID == 1);

            Projects.Add(project2);
        }
        
        private void BuildSequences()
        {
            Sequence sequence1 = new Sequence();
            sequence1.ID = 1;
            sequence1.ExternalId = "c2f5f76a-1ee7-4f92-9150-55de4cefa76f";
            sequence1.Name = "Test Sequence 1";
            sequence1.Description = "Sequence used for testing";
            sequence1.Project = Projects.SingleOrDefault(p => p.ID == 1);
            sequence1.SequenceScripts = new List<SequenceScript>();

            Sequences.Add(sequence1);

            Sequence sequence2 = new Sequence();
            sequence2.ID = 2;
            sequence2.ExternalId = "a019a262-b469-4f8c-9da2-208d3b61ee12";
            sequence2.Name = "Test Sequence 2";
            sequence2.Description = "Another Sequence used for testing";
            sequence2.Project = Projects.SingleOrDefault(p => p.ID == 1);
            sequence2.SequenceScripts = new List<SequenceScript>();

            Sequences.Add(sequence2);
        }

        private void BuildScripts()
        {
            Script script1 = new Script();
            script1.ID = 1;
            script1.ExternalId = "5a768553-052e-47ee-bf48-68f8aaf9cd05";
            script1.Type = ScriptType.Node;
            script1.Name = "Test Script 1";
            script1.Description = "Script used for testing";
            script1.Content = "Test script with [parameter1]";
            script1.Project = Projects.SingleOrDefault(p => p.ID == 1);

            Scripts.Add(script1);

            Script script2 = new Script();
            script2.ID = 2;
            script2.ExternalId = "8ebb4cf0-8e86-4773-9b0d-5ba86d2c0cce";
            script2.Type = ScriptType.Node;
            script2.Name = "Test Script 2";
            script2.Description = "Another Script used for testing";
            script2.Content = "Test script with [parameter2]";
            script2.Project = Projects.SingleOrDefault(p => p.ID == 1);

            Scripts.Add(script2);

            Script script3 = new Script();
            script3.ID = 3;
            script3.ExternalId = "8ebb4cd0-8e36-4778-9b0d-5ba86d9c0cce";
            script3.Type = ScriptType.Node;
            script3.Name = "Test Script 3";
            script3.Description = "Third Script used for testing";
            script3.Content = "Test script without parameters";
            script3.Project = Projects.SingleOrDefault(p => p.ID == 1);

            Scripts.Add(script3);

            Projects.SingleOrDefault(p => p.ID == 1).Scripts.Add(script1);
            Projects.SingleOrDefault(p => p.ID == 1).Scripts.Add(script2);
            Projects.SingleOrDefault(p => p.ID == 1).Scripts.Add(script3);
        }

        private void BuildSequenceScripts()
        {
            SequenceScript sequenceScript1 = new SequenceScript();
            sequenceScript1.ID = 1;
            sequenceScript1.ExternalId = "0cec8cba-3249-44e6-96bb-ff49ac31cdde";
            sequenceScript1.Position = 1;
            sequenceScript1.Sequence = Sequences.SingleOrDefault(s => s.ID == 1);
            sequenceScript1.Name = "Test Sequence Script 1";
            sequenceScript1.Content = "Test script with parameter 1";

            SequenceScripts.Add(sequenceScript1);

            SequenceScript sequenceScript2 = new SequenceScript();
            sequenceScript2.ID = 2;
            sequenceScript2.ExternalId = "d3094a43-b05d-4b3f-8031-50d082239ea3";
            sequenceScript2.Position = 2;
            sequenceScript2.Sequence = Sequences.SingleOrDefault(s => s.ID == 1);
            sequenceScript2.Name = "Test Sequence Script 2";
            sequenceScript2.Content = "Test script with parameter 2";


            SequenceScripts.Add(sequenceScript2);
        }

        private void BuildTasks()
        {
            Task task1 = new Task();
            task1.ID = 1;
            task1.ExternalId = "352e3cf8-480b-4568-80b5-d0cba95dae04";
            task1.Name = "Test Script 1 Task";
            task1.Status = Status.Completed;
            task1.Created = DateTime.Now.AddHours(-2);
            task1.Started = DateTime.Now.AddHours(-1);
            task1.Completed = DateTime.Now;
            task1.ParentId = 1;
            task1.ParentType = ItemType.Script;
            task1.Project = Projects.SingleOrDefault(p => p.ID == 1);
            task1.User = Users.SingleOrDefault(u => u.ID == 1);
            task1.TaskScripts = new List<TaskScript>();

            Tasks.Add(task1);

            TaskScript taskScript1 = new TaskScript();
            taskScript1.ID = 1;
            taskScript1.Content = "Test script with [parameter1]";
            taskScript1.Log = "Output would go in here";
            taskScript1.Task = task1;

            TaskScripts.Add(taskScript1);

            TaskScriptParameter taskParameter1 = new TaskScriptParameter();
            taskParameter1.ID = 1;
            taskParameter1.Name = "parameter1";
            taskParameter1.Value = "Added Parameter";
            taskParameter1.TaskScript = taskScript1;

            TaskScriptParameters.Add(taskParameter1);

            Task task2 = new Task();
            task2.ID = 2;
            task2.ExternalId = "b006de77-5937-486e-baff-f31877cb946e";
            task2.Status = Status.Unprocessed;
            task2.Created = DateTime.Now;
            task2.Started = null;
            task2.Completed = null;
            task2.ParentId = 1;
            task2.ParentType = ItemType.Script;
            task2.Project = Projects.SingleOrDefault(p => p.ID == 1);
            task2.User = Users.SingleOrDefault(u => u.ID == 1);
            task2.TaskScripts = new List<TaskScript>();

            Tasks.Add(task2);

            TaskScript taskScript2 = new TaskScript();
            taskScript2.ID = 2;
            taskScript2.Content = "Test script with [parameter1]";
            taskScript2.Log = null;
            taskScript2.Task = task2;

            TaskScripts.Add(taskScript1);

            TaskScriptParameter taskParameter2 = new TaskScriptParameter();
            taskParameter2.ID = 2;
            taskParameter2.Name = "parameter1";
            taskParameter2.Value = "Other Parameter";
            taskParameter2.TaskScript = taskScript2;

            TaskScriptParameters.Add(taskParameter2);
        }

        public void BuildSchedules()
        {
            Schedule schedule1 = new Schedule();
            schedule1.ID = 1;
            schedule1.ExternalId = "2826018c-69cc-4ead-946b-70bb5a47ab02";
            schedule1.ScheduleType = ScheduleType.Daily;
            schedule1.ScheduleItemType = ItemType.Script;
            schedule1.ScheduleItemId = 1;
            schedule1.Project = Projects.SingleOrDefault(p => p.ID == 1);
            schedule1.ScheduleDetails = new List<ScheduleDetail>();
            schedule1.ScheduleParameters = new List<ScheduleParameter>();

            Schedules.Add(schedule1);
            
            Schedule schedule2 = new Schedule();
            schedule2.ID = 2;
            schedule2.ExternalId = "3b42b7c0-d4fc-4440-93fa-407cb1060768";
            schedule2.ScheduleType = ScheduleType.Weekly;
            schedule2.ScheduleItemType = ItemType.Script;
            schedule2.ScheduleItemId = 2;
            schedule2.Project = Projects.SingleOrDefault(p => p.ID == 1);
            schedule2.ScheduleDetails = new List<ScheduleDetail>();
            schedule2.ScheduleParameters = new List<ScheduleParameter>();

            Schedules.Add(schedule2);

            Schedule schedule3 = new Schedule();
            schedule3.ID = 3;
            schedule3.ExternalId = "b9260c90-62ce-4f41-a448-e766e897a175";
            schedule3.ScheduleType = ScheduleType.Monthly;
            schedule3.ScheduleItemType = ItemType.Sequence;
            schedule3.ScheduleItemId = 1;
            schedule3.Project = Projects.SingleOrDefault(p => p.ID == 1);
            schedule3.ScheduleDetails = new List<ScheduleDetail>();
            schedule3.ScheduleParameters = new List<ScheduleParameter>();

            Schedules.Add(schedule3);
        }

        private void BuildPackageCache()
        {
            PackageCache packageCache1 = new PackageCache();
            packageCache1.ID = 1;
            packageCache1.ExternalId = "5355710e-b947-4762-af19-e78972ef207f";
            packageCache1.Name = "express";
            packageCache1.Version = "4.8.2";
            packageCache1.Store = true;
            packageCache1.Project = Projects.SingleOrDefault(p => p.ID == 1);

            PackageCaches.Add(packageCache1);

            PackageCache packageCache2 = new PackageCache();
            packageCache2.ID = 2;
            packageCache2.ExternalId = "885f4f63-e696-4f0c-bb19-32a0702bc7cc";
            packageCache2.Name = "mongoose";
            packageCache2.Version = "3.8.3";
            packageCache2.Store = true;
            packageCache2.Project = Projects.SingleOrDefault(p => p.ID == 1);

            PackageCaches.Add(packageCache2);
        }

        public int SaveChanges()
        {
            return -1;
        }
    }
}
