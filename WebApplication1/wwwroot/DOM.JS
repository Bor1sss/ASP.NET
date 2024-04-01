function Display(data) {
    $("#error_sect").css('display','none')
    $("main").css('display','block');


    const today = new Date();
    const currentDate = `${today.getDate()}-${today.getMonth() + 1}-${today.getFullYear()}`;
    if (data.cod==404) {
        console.error("Ошибка: Не удалось получить данные о погоде");
        DisplayError();
        return;
    }

    const weatherContainer = $("#Fdata");
    const iconUrl = `http://openweathermap.org/img/wn/${data.weather[0].icon}.png`;
    
    const weatherItem = $(`
        <div class="icon-block">
            <p class="sec-text">Sunny</p>
            <img src="${iconUrl}" alt="weather" />
        </div>
        <div class="temp-block">
            <p><span id="temp-value">${data.main.temp}</span> °C</p>
        </div>
        <div class="add-info">
            <p>Min temperature</p>
            <p> <span id="min-temp">${data.main.temp_min}</span> °C</p>

            <p>Max temperature</p>
            <p> <span id="max-temp">${data.main.temp_max}</span> °C</p>

            <p>Wind speed (km/h)</p>
            <p id="wind-speed">${data.wind.speed}</p>
        </div>
    `);
    
    weatherContainer.empty().append(weatherItem).show();
}

function DisplayHourly(hourlyData) {
    if (hourlyData.cod==404) {
        console.error("Ошибка: Не удалось получить данные о часовой погоде");
        return;
    }

    const weatherContainer = $(".items");
    weatherContainer.empty();

    for (let i = 0; i < 6; i++) {
        const hourData = hourlyData.list[i];
        const iconUrl = `http://openweathermap.org/img/wn/${hourData.weather[0].icon}.png`;
        console.log(hourData.dt_txt)
        const weatherItem = $(`

            </div>
            <div class="items" id="data">
                <div class="weather-item">
                    <p class="sec-text">${hourData.dt_txt.split(" ")[1]}</p>
                    <img src="${iconUrl}" alt="weather" />
                    <p>${hourData.weather[0].main}</p>
                    <p>${hourData.main.temp}</p>
                    <p>${hourData.wind.speed}</p>
                </div>
            </div>
        `);

        weatherContainer.append(weatherItem);
    };

    weatherContainer.show();
}
function DisplayError(){
  $("#error_sect").css('display','flex')
  $("main").css('display','none');
}
export default { Display, DisplayHourly };
