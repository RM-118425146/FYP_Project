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

const names = [];




function renderStats(result) {
    
    let html = '';
    let i = 1;
    console.log(result);
    result.players.forEach(player => {
        let htmlSegment = `<tr>
                                <th scope="row">${i}</th>
                                <td>${player.name}</td>
                                <td>${player.team}</td>
                                <td>${player.cumulative.win_percentage}</td>
                                <td>${player.game_average.core.score}</td>
                                <td>${player.cumulative.core.goals}</td>
                                <td>${player.cumulative.core.saves}</td>
                                <td>${player.game_average.movement.avg_speed}</td>    
                            </tr>`;

        html += htmlSegment;
        i += 1;
        names[1] = player.name;
    });
    
   

    let container = document.querySelector('.statsTable');
    container.innerHTML = html;


    /*
    let options = document.getElementById('.names');
    let html2 = '';
    names.forEach(name => {
        let htmlSegment = '<option value="${name}">${name}</option>';
        html2 += htmlSegment;
    })
    options.innerHTML = html2;
    */
}

