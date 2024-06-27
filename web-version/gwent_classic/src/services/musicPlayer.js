const playlist = [
    {
        title: "The Nightingale",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/so3sgj0erajvsv56ltlao/The-Nightingale-from-The-Witcher-3.mp3?rlkey=9ch2gqmjfhs880x3jrma6r95w&st=s5cqncyy&dl=0"
    },
    {
        title: "Back on the path",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/w0z67ybh2248x4ibotjx0/Back-on-the-Path-Gwent-Tavern.mp3?rlkey=3z4b02r9clwfvl9tj4z2kkudg&st=znxrdy2f&dl=0"
    },
    {
        title: "A Story You Won't Believe",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/hbi3aqu93pvchuq6a6tan/41.-A-Story-You-Won-t-Believe.mp3?rlkey=o7jo48ostw6peuv1qamxn6e9s&st=oq0v72mu&dl=0"   
    },
    {
        title: "Another round for every one",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/404ihggjmliol0ojqwik3/Another-Round-Everyone.mp3?rlkey=ncj3rvfvxul1u34cpb5kn4pm8&st=ycrb5yhr&dl=0"
    },
    {
        title: "Gwent Tavern",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/k5i8ycg8tokiwk4xrfmpo/Witcher-3-GwentTavern-Soundtrack.m4a?rlkey=v0kngz4iudllh9hxvhyzdnbba&st=gt7yr0b2&dl=0"
    }
];

class MusicPlayer {
    #audioPlayer = new Audio();
    currentSongIndex = 0;
    currentSongTime = 0;
    currentSong = null;

    constructor() {
        this.#audioPlayer.autoplay = true;
        this.#audioPlayer.onended = this.#onEndAudio.bind(this);
        this.#audioPlayer.ontimeupdate = () => {
            this.currentSongTime = this.#audioPlayer.currentTime;

            localStorage.setItem('currentSongTime', this.currentSongTime);
        };
        
        this.currentSongIndex = localStorage.getItem('currentSongIndex') ? parseFloat(localStorage.getItem('currentSongIndex')) : 0;
        this.currentSongTime = localStorage.getItem('currentSongTime') ? parseFloat(localStorage.getItem('currentSongTime')) : 0;

        this.setTime(this.currentSongTime);
        
        this.#playSong(this.currentSongIndex, false);
    }

    async play() {        
        if (this.#audioPlayer.paused)
            await this.#audioPlayer.play();
        else
            this.#audioPlayer.pause();
    }

    async nextSong() {
        this.currentSongIndex++;

        if (this.currentSongIndex >= playlist.length)
            this.currentSongIndex = 0;
        
        this.#playSong(this.currentSongIndex);

        localStorage.setItem('currentSongIndex', this.currentSongIndex);
    }

    async previousSong() {
        this.currentSongIndex--;

        if (this.currentSongIndex < 0)
            this.currentSongIndex = playlist.length - 1;

        this.#playSong(this.currentSongIndex);

        localStorage.setItem('currentSongIndex', this.currentSongIndex);
    }

    async #playSong(id, play = true) {
        const song = playlist[id];

        this.currentSong = song;

        this.#audioPlayer.src = song.filePath;
        this.#audioPlayer.load();

        if (play)
            this.play();
    }

    setMuted(state) {
        this.#audioPlayer.muted = state;
    }

    setVolume(volume) {
        this.#audioPlayer.volume = volume / 100;
    }

    /**
     * @param {number} time - time in seconds 
     */
    setTime(time) {
        this.#audioPlayer.currentTime = time;
    }

    #onEndAudio() {
        this.nextSong();
    }
}

const musicPlayer = new MusicPlayer();

export default musicPlayer;