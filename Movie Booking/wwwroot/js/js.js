fetch("https://simonsmoviebooking.azurewebsites.net/api/movie")
    .then(response => response.json())
    .then(movies => {
        for (i = 0; i < movies.length; i++) {
            var y = document.createElement("TR")
            y.setAttribute("id", "movie" + movies[i].id)
            document.getElementById("tbody").appendChild(y)

            var z = document.createElement("TD");
            var t = document.createTextNode(movies[i].title);
            z.appendChild(t);
            document.getElementById("movie" + movies[i].id).appendChild(z);

            var z = document.createElement("TD");
            var t = document.createTextNode(movies[i].genre);
            z.appendChild(t);
            document.getElementById("movie" + movies[i].id).appendChild(z);

            var z = document.createElement("TD");
            var t = document.createTextNode(movies[i].description);
            z.appendChild(t);
            document.getElementById("movie" + movies[i].id).appendChild(z);

            var z = document.createElement("TD");
            var t = document.createTextNode(movies[i].numSeats);
            z.appendChild(t);
            document.getElementById("movie" + movies[i].id).appendChild(z);

            var btn = document.createElement("BUTTON");
            btn.innerHTML = "Bestil";
            btn.setAttribute("onclick", "orderMovie(" + movies[i].id + ")")
            var z = document.createElement("TD");
            z.appendChild(btn);
            document.getElementById("movie" + movies[i].id).appendChild(z);
        }
    })
    .catch (error => console.error('Unable to get items.', error));


function orderMovie(movieNumber) {

}