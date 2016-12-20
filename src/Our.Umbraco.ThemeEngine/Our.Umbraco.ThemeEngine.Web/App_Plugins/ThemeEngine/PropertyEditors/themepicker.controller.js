angular.module("umbraco")
    .controller("Our.Umbraco.ThemeEngine.ThemePickerController",
    function ($scope, umbRequestHelper, $http) {
        var url = "api/ThemeEnginePropertyEditorApi/GetThemes";

        umbRequestHelper.resourcePromise(
                $http.get(url),
                'Failed to retrieve themes for Site at ' + url)
            .then(function (data) {
                $scope.themes = data;
            });
    });