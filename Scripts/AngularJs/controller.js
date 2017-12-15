angular.module("app")
.controller("ListController", function ($scope, $http) {
    $http.get("api/users")
    .success(function (data) {
        $scope.Users = data;
    })
    .error(function (err) {
        console.log(err)
    });

    $scope.message = "mensaje desde lisController";
})
.controller("EditController", function ($scope, $http, $routeParams,$location) {
    var id = 0;

    var getUserById = function () {

        $http.get('api/Users/' + $routeParams.id)
        .then(function (response) {
            $scope.nombre = response.data.nombre;
            $scope.selectedCargo = response.data.codigo_cargo;
            $scope.selectedCountry = response.data.id_country;
            $scope.selectedCity = response.data.id_city;
            $scope.GetStates();
        },
        function (error) {
            console.log(error);
        });

    };



    $scope.GetStates = function () {
        var country = $scope.selectedCountry;

        

        if (country) {
            $http.get('api/City/' + country)
            .then(function (response) {
                $scope.cities = response.data;
            },
            function (err) {
                console.log('Hubo un error en la captura de ciudades');
            });
        }
        else {
            $scope.cities = null;
        }
    };



    $scope.save = function () {
        

        var obj = {
            codigo_usuario: id,
            nombre: $scope.nombre,
            codigo_cargo: $scope.selectedCargo,
            id_country: $scope.selectedCountry,
            id_city: $scope.selectedCity
        }


        if (id == 0) {
            $http.post('api/Users', obj)
            .then(function (response) {
                alert("datos insertados correctamente");
                $location.path('/list');
            },
            function (error) {
                console.log(error);
            });
        }
        else
        {
            $http.put('api/Users/' + $routeParams.id, obj)
            .then(function (response) {
                alert("datos actualizados correctamente");
                $location.path('/list');
            },
            function (error) {
                console.log(error);
            });
        }
    }


    if ($routeParams.id) {

        id = $routeParams.id;

        getUserById();

        $scope.title = "Edit";
    }
    else
    {
        $scope.title = "Create";
    }

    $http.get("api/Cargos")
    .success(function (data) {
        $scope.Cargos = data;
    })
    .error(function (err) {
        console.log(err)
    });

    $scope.id = 0;

    $http.get("api/Country")
    .success(function (data) {

        $scope.countries = data;

    })
    .error(function (err) {
        console.log('la vaina dio un error');
    });

    $scope.message = "mensaje desde lisController";

})
.controller("DeleteController", function ($scope, $http, $routeParams,$location) {
    var getUserById = function () {

        $http.get('api/Users/' + $routeParams.id)
        .then(function (response) {
            $scope.nombre = response.data.nombre;
            $scope.cargo = response.data.desc_cargo;
            $scope.selectedCountry = response.data.country;
            $scope.selectedCity = response.data.city;

            console.log(response.data);
        },
        function (error) {
            console.log(error);
        });

    };

    if ($routeParams.id)
    {
        getUserById();
    }

    $scope.delete = function () {
        $http.delete('api/Users/' + $routeParams.id)
        .then(function (response) {
            alert('Registro eliminado correctamente');
            $location.path('/list');
        }, function (error) {
            console.log('Hubo un error al momento de eliminar el registro');
        });
    };
});
           