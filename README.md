## Installation

Install [.NET](https://dotnet.microsoft.com/download) to run this program

```bash
dotnet build
```
> This allows all the relevant packages to be installed for the project to work correctly

Please ensure that Port 5000 is open on your local machine, run the following command to start the program

```bash
dotnet run
```

## Tasks

#### POST /metadata
Saves a new piece of metadata.

- [x] Movie Id - integer representing unique movie identifier 
- [x] Title - string representing the title of the Movie
- [x] Language - string representing the translation of the metadata
- [x] Duration - string representing the length of the movie
- [x] Release Year - integer representing the year the movie was released

#### GET /metadata/:movieId
Returns all metadata for a given movie.
- [x] Only the latest piece of metadata (highest Id) should be returned where there are
multiple metadata records for a given language.
- [x] Only metadata with all data fields present should be returned, otherwise it should be
considered invalid.
- [x] If no metadata has been POSTed for a specified movie, a 404 should be returned.
- [x] Results are ordered alphabetically by language.

#### GET /movies/stats
Returns the viewing statistics for all movies.
- [x] The movies are ordered by most watched, then by release year with newer releases
being considered more important.
- [x] The data returned only needs to contain information from the supplied csv documents
and does not need to return data provided by the POST metadata endpoint.