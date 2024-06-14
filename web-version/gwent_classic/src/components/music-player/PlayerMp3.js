let playlist = [
    {
        title: "The Nightingale",
        file: "/assets/songs/The%20Nightingale%20from%20The%20Witcher%203.mp3"
    },
    {
        title: "Back on the path",
        file: "/assets/songs/Back%20on%20the%20Path%20Gwent%20Tavern.mp3"
    }
];

let currentSongIndex = 0;
let audioPlayer = new Audio();
let currentTimecode = "00:00";

audioPlayer.addEventListener('error', function (e) {
    console.error('Audio error:', e);
    console.log('Error details:', audioPlayer.error);
});

function play() {
    if (audioPlayer.paused) {
        audioPlayer.play().then(() => {
            console.log("playing");
        }).catch(error => {
            console.log("Error during playback:", error);
        });
    } else {
        audioPlayer.pause();
        console.log("paused");
    }
}

function next() {
    currentSongIndex++;
    if (currentSongIndex >= playlist.length) {
        currentSongIndex = 0;
    }
    playSong(currentSongIndex);
}

function previous() {
    currentSongIndex--;
    if (currentSongIndex < 0) {
        currentSongIndex = playlist.length - 1;
    }
    playSong(currentSongIndex);
}

function playSong(index) {
    let song = playlist[index];
    console.log("Loading audio file:", song.file);
    audioPlayer.src = song.file;
    audioPlayer.currentTime = 0;
    audioPlayer.load();
    audioPlayer.play().then(() => {
        console.log("playing");
    }).catch(error => {
        console.log("Error during playback:", error);
    });
    currentTimecode = "00:00";
    audioPlayer.addEventListener('timeupdate', updateTimecode);
}

function updateTimecode() {
    let minutes = Math.floor(audioPlayer.currentTime / 60);
    let seconds = Math.floor(audioPlayer.currentTime % 60);
    currentTimecode = `${minutes}:${seconds.toString().padStart(2, '0')}`;
    console.log(currentTimecode);
}

function setVolume(volume) {
    audioPlayer.volume = volume / 100;
}

function getCurrentTimecode() {
    return currentTimecode;
}

audioPlayer.onended = next;

export { play, next, previous, setVolume, getCurrentTimecode };