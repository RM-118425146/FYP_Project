var myHeaders = new Headers();
myHeaders.append("Authorization", "qFFndsJVqp345GV1rkjwfYx6Tbnh53fBIhtsukqI");

var requestOptions = {
    method: 'GET',
    headers: myHeaders,
    redirect: 'follow'
};

const proxyUrl = 'https://cors-anywhere.herokuapp.com/';
const apiUrl = "https://ballchasing.com/api/groups/legion-winter-jrj9l32d83"

fetch(proxyUrl + apiUrl, requestOptions)
    .then(response => response.json())
    .then(result => { renderStats(result); })
    .catch(error => console.log('error', error));

function renderStats(result) {

    document.getElementById('loading').style.visibility = 'hidden';
   
    $(document).ready(function () {
        $('#statsTable').DataTable({
            responsive: true,
            keys: true,
            fixedHeader: true,
            data: result.players,
            columns: [
                { data: 'name', title: 'Player Name' },
                { data: 'team', title: 'Team Name' },
                { data: 'cumulative.win_percentage', title: 'Win %' },
                { data: 'game_average.core.score', title: 'Average Score' },
                { data: 'cumulative.core.goals', title: 'Total Goals Scored' },
                { data: 'cumulative.core.saves', title: 'Total Saves Made' },
                { data: 'game_average.movement.avg_speed', title: 'Average Car Speed' }
            ],
            columnDefs: [{
                targets: 0,
                data: 'name',
                render: function (data, type, full, meta) {
                    return '<a href="/RocketLeague/Player/' + data + '">' + data + '</a>';
                }
            }]
        });
    });

}

