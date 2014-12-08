using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostRunner.SL;
using GhostRunner.Models;
using System.Collections.Generic;

namespace GhostRunner.Tests.SL
{
    [TestClass]
    public class ProjectServiceTests
    {
        private ProjectService _projectService;

        [TestInitialize]
        public void Initialize()
        {
            _projectService = new ProjectService(new TestContext());
        }

        #region Project Methods

        [TestMethod]
        public void GetAllProjects()
        {
            IList<Project> projects = _projectService.GetAllProjects();
            Assert.AreEqual(2, projects.Count);
        }

        [TestMethod]
        public void GetProject()
        {
            Project project1 = _projectService.GetProject(1);
            Assert.IsNotNull(project1);
            Assert.AreEqual(1, project1.ID);

            Project project99 = _projectService.GetProject(99);
            Assert.IsNull(project99);
        }
        
        [TestMethod]
        public void InsertProject()
        {
            IList<Project> projectsBefore = _projectService.GetAllProjects();
            Assert.AreEqual(2, projectsBefore.Count);

            Project newProject = _projectService.InsertProject(1, "New Test Project");
            Assert.IsNotNull(newProject);
            Assert.AreEqual("New Test Project", newProject.Name);

            IList<Project> projectsAfter = _projectService.GetAllProjects();
            Assert.AreEqual(3, projectsAfter.Count);

            Project newProject99 = _projectService.InsertProject(99, "New Test Project");
            Assert.IsNull(newProject99);
        }

        [TestMethod]
        public void UpdateProject()
        {
            Project project1 = _projectService.GetProject(1);
            Assert.IsNotNull(project1);
            Assert.AreEqual("Test Project 1", project1.Name);

            Boolean updateSuccessful = _projectService.UpdateProject(project1.ExternalId, "New Project Name");
            Assert.IsTrue(updateSuccessful);

            Project updatedProject1 = _projectService.GetProject(1);
            Assert.IsNotNull(updatedProject1);
            Assert.AreEqual("New Project Name", updatedProject1.Name);
        }

        [TestMethod]
        public void DeleteteProject()
        {
            Boolean deleteSuccessful = _projectService.DeleteProject("d4708c0d-721e-426e-b49e-35990687db22");
            Assert.IsTrue(deleteSuccessful);

            Project project1 = _projectService.GetProject(1);
            Assert.IsNull(project1);
        }

        #endregion
    }
}
