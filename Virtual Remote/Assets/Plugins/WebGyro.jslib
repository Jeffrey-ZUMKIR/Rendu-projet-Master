

mergeInto(LibraryManager.library, {

    /*GetGyroscopeRotation: function(x, y, z) {
        // Appelle la fonction de rotation et récupère les valeurs
        var rotations = { x: 0, y: 0, z: 0 };
        DeviceOrientationEvent.requestPermission();
        if (typeof DeviceOrientationEvent !== 'undefined' && typeof DeviceOrientationEvent.requestPermission === 'function') {
            DeviceOrientationEvent.requestPermission().then(permissionState => {
                if (permissionState === 'granted') {
                    window.addEventListener('deviceorientation', (event) => {
                        rotations.x = event.alpha;
                        rotations.y = event.beta;
                        rotations.z = event.gamma;
                    });
                }
            }).catch(console.error);
        } else {
            window.addEventListener('deviceorientation', (event) => {
                rotations.x = event.alpha;
                rotations.y = event.beta;
                rotations.z = event.gamma;
            });
        }

        // Utilisez les valeurs x, y, et z dans votre logique
        x = rotations.x;
        y = rotations.y;
        z = rotations.z;

        // Exemple : Affichez les valeurs dans la console
        console.log("Rotation X:", x);
        console.log("Rotation Y:", y);
        console.log("Rotation Z:", z);
    },*/


    

    Hello: function () {
        window.alert("Hello, world!");
    },
});


