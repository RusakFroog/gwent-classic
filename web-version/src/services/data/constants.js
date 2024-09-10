export const HTTP_SERVER = import.meta.env.MODE === "production" ? 'https://known-tough-doe.ngrok-free.app/api' : "http://localhost:5187/api";
export const CDN_URL = "https://cdn.jsdelivr.net/gh/Rusakfroog/gwent-classic/cdn";
export const FRACTIONS = {
    Monsters: "Monsters",
    Nilfgaardian: "Nilfgaardian Empire",
    NorthernRealms: "Northern Realms",
    Scoiatael: "Scoia'tael",
    Skellige: "Skellige",
};