var RootApiPath = "https://localhost:7223"
var connection = new signalR.HubConnectionBuilder().withUrl(RootApiPath + "/NeuralNetworkTraining").build();

var chart;
var start = Date.now();
var counter = 0;
$(document).ready(function () {
    RefreshNeuralNetworkListSelect();
    RefreshDatasetsListSelect();
    signalRSetup();
});
jQuery(window).bind(
    "beforeunload",
    function () {
        connection.client.stopClient = function () {
            $.connection.hub.stop();
        };
        return confirm("Do you really want to close?")
        
    }
)
function signalRSetup() {
    connection.start().then(function () {
    }).catch(function (err) {
        return console.error(err.toString());
    });
    connection.on("TrainIterateOnce", function (Data) {
        var inputsAsString = "Result" + Data.dataSet.inputs.map(i => i).join().replace(",", "");
        $("#" + inputsAsString).find(".meanError").width(CalculatePercent(Data.deltasErrors) + "%");
    });
    connection.on("Train_finished", function () {
        alert("Your neural network has been train you can now use it !");
    });
}
function TrainNeuralNetwork() {
    var label;
    start = Date.now();
    $.ajax({
        url: RootApiPath + "/DataSetsList/get/" + $("#Datasets_select").val(),
        type: "GET",
        success: function (result) {
            $("#RowTemplaterContainer").html("");
            $("#meanError").width("100%");
            for (var i = 0; i < result.length; i++) {
                var templateCopy = $("#templateMeanError").clone();
                var inputsAsString = result[i].inputs.map(i => i).join();

                $(templateCopy.html().replace("%dataname%", inputsAsString))
                    .attr("id", "Result" +  inputsAsString.replace(",",""))
                    .appendTo("#RowTemplaterContainer");
            }

            result.forEach(r => $("#RowTemplaterContainer").append(templateCopy));

            connection.invoke("TrainNeuralNetwork", $("#NeuralNetwork_to_train_select").val(), $("#Datasets_select").val()).catch(function (err) {
                return console.error(err.toString());
            });
        },
        error: function (err) {
            console.log(err);
        }
    });

}

function RefreshNeuralNetworkListSelect() {
    $.ajax({
        url: RootApiPath + "/NeuralNetwork/get/%20/0/10000",
        type: "GET",
        contentType: "text/plain",
        success: function (result) {
            $.each(result, function (i, item) {
                $("#NeuralNetwork_to_train_select").append($('<option>', {
                    value: item,
                    text: item
                }));
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function RefreshDatasetsListSelect() {
    $.ajax({
        url: RootApiPath + "/DataSetsList/get/%20/0/10000",
        type: "GET",
        contentType: "text/plain",
        success: function (result) {
            $.each(result, function (i, item) {
                $("#Datasets_select").append($('<option>', {
                    value: item,
                    text: item
                }));
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function CalculatePercent(array) {
    var mean, total, count;
    total = 0;
    count = 0;
    jQuery.each(array, function (index, value) {
        total += value;
        count++;
    });

    mean = total / count;

    if (mean < 0) { mean *= -1 }
    return mean * 100;
}

