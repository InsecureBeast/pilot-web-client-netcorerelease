﻿using Ascon.Pilot.Core;
using Ascon.Pilot.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ascon.Pilot.WebClient.ViewModels
{
    public class TaskDetailsViewModel
    {
        public TaskDetailsViewModel(Guid taskId, IRepository repository)
        {
            var obj = repository.GetObjects(new[] { taskId });
            var task = new DTask(obj.First(), repository);
            Title = task.Title;
            Description = task.Description;
            Initiator = task.GetInitiatorDisplayName(repository);
            Executor = task.GetExecutorDisplayName(repository);
            Attachments = repository.GetObjects(task.InitiatorAttachments.ToArray()).Select(a => new Attachment(a, repository));
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Initiator { get; private set; }
        public string Executor { get; private set; }
        public IEnumerable<Attachment> Attachments { get; private set; }
    }

    public class Attachment
    {
        public Attachment(DObject obj, IRepository repository)
        {
            var type = repository.GetType(obj.TypeId);
            Title = obj.GetTitle(type);
        }

        public string Title { get; private set; }
        public string FileExtension { get; private set; }
    }
}
