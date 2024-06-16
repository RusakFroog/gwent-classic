const playlist = [
    {
        title: "The Nightingale",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/so3sgj0erajvsv56ltlao/The-Nightingale-from-The-Witcher-3.mp3?rlkey=9ch2gqmjfhs880x3jrma6r95w&st=s5cqncyy&dl=0"
    },
    {
        title: "Back on the path",
        filePath: "https://dl.dropboxusercontent.com/scl/fi/w0z67ybh2248x4ibotjx0/Back-on-the-Path-Gwent-Tavern.mp3?rlkey=3z4b02r9clwfvl9tj4z2kkudg&st=znxrdy2f&dl=0"
    }
];

class MusicPlayer {
    #audioPlayer = new Audio();
    currentSongIndex = 0;
    currentSongTime = 0;
    currentSong = null;

    savedSongTime = localStorage.getItem('currentSongTime');
    savedSongIndex = localStorage.getItem('currentSongIndex');

    constructor() {
        this.#audioPlayer.onended = this.onEndAudio.bind(this);
        this.#audioPlayer.ontimeupdate = () => {
            this.currentSongTime = this.#audioPlayer.currentTime;
            if (this.currentSongTime > 0) {
                localStorage.setItem('currentSongTime', this.currentSongTime.toString());
                console.log(this.currentSongTime);
            }
        };
        
        if (this.currentSong === null && this.savedSongIndex === null)
            this.#playSong(0, false);

        else
            this.currentSongIndex = parseInt(this.savedSongIndex);
            this.#playSong(this.savedSongIndex, false);

        if (this.savedSongTime !== null)
            this.setTime(this.savedSongTime);
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

        this.seveCurrentSongIndex();
    }

    async previousSong() {
        this.currentSongIndex--;

        if (this.currentSongIndex < 0)
            this.currentSongIndex = playlist.length - 1;

        this.#playSong(this.currentSongIndex);

        this.seveCurrentSongIndex();
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
        const v = (volume / 100);

        console.log(v);
        this.#audioPlayer.volume = v;
    }

    /**
     * @param {number} time - time in seconds 
     */
    setTime(time) {
        this.#audioPlayer.currentTime = time;
    }

    onEndAudio() {
        this.nextSong();
    }

    seveCurrentSongIndex() {
        localStorage.setItem('currentSongIndex', this.currentSongIndex.toString());
    }
}

const musicPlayer = new MusicPlayer();

export default musicPlayer;