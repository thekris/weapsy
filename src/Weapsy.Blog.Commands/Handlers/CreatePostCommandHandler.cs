﻿using System;
using FluentValidation;
using Weapsy.Blog.Commands.Handlers.Contracts;
using Weapsy.Blog.Domain.Post;

namespace Weapsy.Blog.Commands.Handlers
{
	public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand>
	{
		private readonly IPostRepository _postRepository;
		private readonly IValidator<CreatePostCommand> _validator;

		public CreatePostCommandHandler(IPostRepository postRepository, IValidator<CreatePostCommand> validator)
		{
			_postRepository = postRepository;
			_validator = validator;
		}

		public void Handle(CreatePostCommand command)
		{
			var validationResult = _validator.Validate(command);

			if (!validationResult.IsValid)
			{
				var failures = validationResult.Errors;
				throw new InvalidOperationException();
			}

			var post = Post.CreateNew(command.BlogId, command.Title, command.Content, command.Published, command.Categories, command.Tags);

			_postRepository.Save(post);
		}
	}
}
