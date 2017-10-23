using System;
using System.IO;
using System.Runtime.CompilerServices;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Git.Extensions;
using LibGit2Sharp;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
namespace Cake.Git
{
    // ReSharper disable once PublicMembersMustHaveComments
    public static partial class GitAliases
    {
        /// <summary>
        /// Download objects and refs from another repository.
        /// </summary>
        /// <example>
        /// <code>
        ///     var result = GitFetch("update-1");
        /// </code>
        /// </example>
        /// <param name="remoteName">The name of the remote.</param>
        /// <returns>the status of the fetch.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Fetch")]
        public static GitMergeResult GitFetch(
            this ICakeContext context,
            string remoteName,
            )
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var workFullDirectoryPath = repositoryDirectoryPath.MakeAbsolute(context.Environment);

            if (!context.FileSystem.Exist(workFullDirectoryPath))
            {
                throw new DirectoryNotFoundException($"Failed to find workDirectoryPath: {workFullDirectoryPath}");
            }

            return new GitMergeResult(
                context.UseRepository(
                    repositoryDirectoryPath,
                    repository =>
                        Commands.Fetch(
                            repository,
                            new Signature(
                                remoteName,
                                DateTimeOffset.Now
                            ),
                            new FetchOptions()
                        )
               )
            );
        }
    }
}
