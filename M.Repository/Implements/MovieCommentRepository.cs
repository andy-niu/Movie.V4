using M.Repository.Entity;
using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace M.Repository.Implements
{
    ///<summary>
    ///MovieComment
    ///</summary>
    public class MovieCommentRepository : BaseRepository<Entity.MovieComment> ,Interfaces.IMovieCommentRepository
    {
        private readonly ILogger _logger;
        public MovieCommentRepository(ILogger<MovieCommentRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }

    }
}	 
