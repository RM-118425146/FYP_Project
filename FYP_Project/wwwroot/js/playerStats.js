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

    let positionData;
    let boostData;

    document.getElementById('loading').style.visibility = 'hidden';

    let name = document.getElementById("name").value;
    console.log(name);
    let i = 1;
    let html = '';
    result.players.forEach(player => {
        if (player.name == name) {
            let htmlSegment = `<tr>
                                <td>${player.name}</td>
                                <td>${player.team}</td>
                                <td>${player.cumulative.games}</td>
                                <td>${player.cumulative.win_percentage}</td>
                                <td>${player.game_average.core.goals}</td>
                                <td>${player.game_average.core.assists}</td>
                                <td>${player.game_average.core.saves}</td>
                                <td>${player.game_average.core.score}</td>
                                <td>${player.game_average.demo.inflicted}</td>
                                <td>${player.game_average.demo.taken}</td>
                                <td>${player.game_average.boost.avg_amount}</td>
                                <td>${player.game_average.boost.time_zero_boost}</td>
                                <td>${player.game_average.boost.amount_used_while_supersonic}</td>
                                <td>${player.game_average.movement.avg_speed}</td>
                                <td>${player.game_average.positioning.avg_distance_to_ball}</td>
                            </tr>`;

            let averagePosition = [player.game_average.positioning.percent_defensive_third, player.game_average.positioning.percent_neutral_third, player.game_average.positioning.percent_offensive_third];
            positionData = {
                labels: ['% Defensive 1/3', '% Neutral 1/3', '% Offensive 1/3'],
                datasets: [
                    {
                        label: 'Average Position on pitch',
                        data: averagePosition,
                        backgroundColor: [
                            'rgba(255, 0, 34, 0.9)',
                            'rgba(29, 0, 255, 0.9)',
                            'rgba(49, 166, 3, 1)'
                        ]
                    }
                ]
            };

            let boostStats = [player.game_average.boost.bpm, player.game_average.boost.bcpm, player.game_average.boost.avg_amount, player.game_average.boost.count_collected_big, player.game_average.boost.count_stolen_big, player.game_average.boost.count_collected_small, player.game_average.boost.count_stolen_small];
            boostData = {
                labels: ['Average Boost Per Minute', 'Average Boost Collected Per Minute', 'Average Boost Amount', 'Average Big Pads Collected', 'Average Big Pads Stolen', 'Average Small Pads Collected', 'Average Small Pads Stolen'],
                datasets: [
                    {
                        label: 'Average Position on pitch',
                        data: boostStats,
                        backgroundColor: [
                            'rgba(255, 0, 34, 0.9)',
                            'rgba(29, 0, 255, 0.9)',
                            'rgba(49, 166, 3, 1)',
                            'rgba(255, 0, 34, 0.9)',
                            'rgba(29, 0, 255, 0.9)',
                            'rgba(49, 166, 3, 1)',
                            'rgba(255, 0, 34, 0.9)'
                        ]
                    }
                ]
            };

            html += htmlSegment;
            i++;
        }
    });

    
   
    let container = document.querySelector('.statsTableBody');
    container.innerHTML = html;

    $(document).ready(function () {
        $('#statsTable').DataTable({
            responsive: true,
            keys: true,
            fixedHeader: true
        });
    });

    //chart.js script

    const boostConfig = {
        type: 'bar',
        data: boostData,
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: 'Boost Stats'
                }
            }
        },
    };

    const boostStats = new Chart(
        document.getElementById('boostChart'),
        boostConfig
    );

    const averagePosisitionConfig = {
        type: 'pie',
        data: positionData,
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: 'Area of the pitch %'
                }
            }
        }
    };

    const averagePosisition = new Chart(
        document.getElementById('averagePosisition'),
        averagePosisitionConfig
    );

}

