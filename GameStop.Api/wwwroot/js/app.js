async function load()
{
    let response =
        await fetch("/api/VideoGame");
    let data =
        await response.json();
    let html = "";
    data.forEach(g => {
        html += `
        <div class="card">
        <h3>${g.name}</h3>
        <p class="publisher">${g.publisher} - ${g.developer}</p>
        <p class="prezzo">€ ${g.price}</p>
        <p class="copie">Copie disponibili: ${g.copiesCount}</p>
        <div class="card-buttons">
        <button onclick="edit(${g.id}, '${g.name}', '${g.publisher}', '${g.developer}', ${g.copiesCount}, ${g.price})">
        Modifica
        </button>
        <button class="btn-delete" onclick="remove(${g.id})">
        Elimina
        </button>
        </div>
        </div>
        `;
    });

    document.getElementById("catalogoGrid")
        .innerHTML = html;
}

async function save()
{
    let game =
        {
            name:
            document.getElementById("name").value,
            publisher:
            document.getElementById("publisher").value,
            developer:
            document.getElementById("developer").value,
            copiesCount:
            parseInt(document.getElementById("copiesCount").value),
            price:
            parseFloat(document.getElementById("price").value)
        };

    let id = document.getElementById("gameId").value;

    if (id == "")
    {
        // nuovo gioco
        await fetch("/api/VideoGame",
            {
                method: "POST",
                headers:
                    {
                        "Content-Type": "application/json"
                    },
                body:
                    JSON.stringify(game)
            });
    }
    else
    {
        // modifica gioco esistente
        game.id = parseInt(id);
        await fetch("/api/VideoGame",
            {
                method: "PUT",
                headers:
                    {
                        "Content-Type": "application/json"
                    },
                body:
                    JSON.stringify(game)
            });
    }

    clearForm();
    load();
}

function edit(id, name, publisher, developer, copiesCount, price)
{
    document.getElementById("gameId").value = id;
    document.getElementById("name").value = name;
    document.getElementById("publisher").value = publisher;
    document.getElementById("developer").value = developer;
    document.getElementById("copiesCount").value = copiesCount;
    document.getElementById("price").value = price;
}

async function remove(id)
{
    await fetch("/api/VideoGame/" + id,
        {
            method: "DELETE"
        });
    load();
}

function clearForm()
{
    document.getElementById("gameId").value = "";
    document.getElementById("name").value = "";
    document.getElementById("publisher").value = "";
    document.getElementById("developer").value = "";
    document.getElementById("copiesCount").value = "";
    document.getElementById("price").value = "";
}

load();
