namespace Smdb.Api;

using Shared.Http;
using Smdb.Api.Movies;
using Smdb.Api.Users;
using Smdb.Api.Actors;
using Smdb.Api.ActorsMovies;
using Smdb.Core.Movies;
using Smdb.Core.Users;
using Smdb.Core.Actors;
using Smdb.Core.ActorsMovies;
using Smdb.Core.Db;



public class App : HttpServer
{
	private object actorRouter;

	public override void Init()
	{

		var db = new MemoryDatabase();
		//movies
		var movieRepo = new MemoryMovieRepository(db);
		var movieServ = new DefaultMovieService(movieRepo);
		var movieCtrl = new MoviesController(movieServ);
		var movieRouter = new MoviesRouter(movieCtrl);

		// USERS
		var userRepo = new MemoryUserRepository(db);
		var userServ = new DefaultUserService(userRepo);
		var userCtrl = new UsersController(userServ);
		var userRouter = new UsersRouter(userCtrl);


		// ACTORS-MOVIES
		var amRepo = new MemoryActorMovieRepository(db);
		var amServ = new DefaultActorMovieService(amRepo);
		var amCtrl = new ActorsMoviesController(amServ);
		var amRouter = new ActorsMoviesRouter(amCtrl);


		var apiRouter = new HttpRouter();


		router.Use(HttpUtils.StructuredLogging);
		router.Use(HttpUtils.CentralizedErrorHandling);
		router.Use(HttpUtils.AddResponseCorsHeaders);
		router.Use(HttpUtils.DefaultResponse);
		router.Use(HttpUtils.ParseRequestUrl);
		router.Use(HttpUtils.ParseRequestQueryString);
		router.UseParametrizedRouteMatching();
		router.UseRouter("/api/v1", apiRouter);

		apiRouter.UseRouter("/movies", movieRouter);
		apiRouter.UseRouter("/users", userRouter);
		apiRouter.UseRouter("/actors", actorRouter);
		apiRouter.UseRouter("/actors-movies", amRouter);

		// GET /api/v1/movies/
		// POST /api/v1/movies/
		// PUT /api/v1/movies/1
		// DEL /api/v1/movies/1
	}
}