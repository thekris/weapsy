﻿using System;
using Weapsy.Blog.Domain.Blog.Events;
using Weapsy.Blog.Domain.Blog.Exceptions;

namespace Weapsy.Blog.Domain.Blog
{
    public class Blog : AggregateRoot
    {
        public string Title { get; private set; }

        public Blog()
        {
            Id = Guid.Empty;
        }

        private Blog(string title) : this()
        {
            Title = title;

            Id = Guid.NewGuid();
            MarkNew();

			Events.Add(new BlogCreatedEvent(Id, Title));
		}

        public static Blog CreateNew(string title)
        {
            return new Blog(title);
        }

        public void ChangeTitle(string title)
        {
            IsBlogCreated();

            Title = title;

            MarkOld();

			Events.Add(new BlogTitleChangedEvent(Id, Title));
		}

        private void IsBlogCreated()
        {
            if (Id == Guid.Empty)
            {
                throw new BlogNotCreatedException("The Post is not created and no opperations can be executed on it.");
            }
        }
    }
}
