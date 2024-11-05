<script>
import Button from '../components/ui/Button.vue';
import Card from '../components/decks/Card.vue';
import Notification from '../components/ui/Notification.vue';
import { FRACTIONS } from '../services/data/constants';
import { getDeck as getDeckByFraction, updateDeck } from '../services/Decks.js';

export default {
    name: "DecksPage",
    components: {
        Button,
        Card,
        Notification
    },

    data() {
        class Fraction {
            constructor(name, leader) {
                this.name = name;
                this.leader = leader;
            }

            get imagePath() {
                return `/fractions/${this.name.toLowerCase()}.png`;
            }

            get leaderImagePath() {
                return `/leaders/${this.leader.toLowerCase()}.png`;
            }
        }

        return {
            error: {
                text: "" 
            },

            currentFractionId: 0,

            fractions: [
                new Fraction("NorthernRealms", "commander"),
                new Fraction("Nilfgaardian", "emperor"),
                new Fraction("Monsters", "beast"),
                new Fraction("Skellige", "raider"),
                new Fraction("Scoiatael", "archer"),
            ],

            aboutFractions: { 
                "NorthernRealms": "Draw a card from your deck whenever you win a round.",
                "Nilfgaardian": "Win whenever there is a draw.",
                "Monsters": "One randomly-chosen Monsters Unit Card stays on the battlefield after each round.",
                "Skellige": "2 random cards from the graveyard are placed on the battlefield at the start of the third round.",
                "Scoiatael": "You decide who goes first at the start of battle.",
            },

            categoriesName: [
                { All: "ALL CARDS" },
                { Closer: "CLOSE COMBAT UNIT CARDS" }, 
                { Ranger: "RANGED UNIT CARDS" }, 
                { Siege: "SIEGE UNIT CARDS" }, 
                { Hero: "HERO CARDS" }, 
                { Weather: "WEATHER CARDS" }, 
                { Special: "SPECIAL CARDS" }, 
            ],

            stats: {
                cardsInDeck: 0,
                unitCards: 0,
                specialCards: 0,
                unitStrength: 0,
                heroCards: 0
            },

            maxStats: {
                unitCards: 22,
                specialCards: 10
            },

            selectedRightCategory: 'All',
            selectedLeftCategory: 'All',

            cardsInDeck: [
                {
                    id: 2,
                    lines: ['Ranger', 'Closer'],
                    category: 'Hero'
                },
                {
                    id: 1,
                    lines: ['Siege'],
                    category: 'Hero'
                },
                {
                    id: 3,
                    lines: ['Closer'],
                    category: 'Hero'
                },
                {
                    id: 4,
                    lines: ['Closer'],
                    category: 'Hero'
                },
                {
                    id: 5,
                    lines: ['Closer'],
                    category: 'Hero'
                },
                {
                    id: 6,
                    lines: ['Closer'],
                    category: 'Hero'
                },
                {
                    id: 12,
                    lines: ['Closer'],
                    category: 'Hero'
                },
                {
                    id: 14,
                    lines: ['Closer'],
                    category: 'Hero'
                },
            ],
            cardsInPool: [
                {
                    id: 17,
                    lines: ['Closer'],
                    category: 'Hero'
                },
            ],
        };
    },

    mounted() {
        this.getDeck(this.fractions[this.currentFractionId].name);
    },

    unmounted() {
        updateDeck(this.fractions[this.currentFractionId].name, this.cardsInDeck.map(c => c.id));
    },

    computed: {
        getCurrentFraction() {
            return this.fractions[this.currentFractionId];
        },

        getCurrentFractionLeader() {
            return this.getCurrentFraction.leaderImagePath;
        },

        getCurrentFractionName() {
            return FRACTIONS[this.getCurrentFraction.name];
        },

        getPreviousFraction() {
            const prevId = (this.currentFractionId - 1 + this.fractions.length) % this.fractions.length;

            return FRACTIONS[this.fractions[prevId].name];
        },

        getNextFraction() {
            const nextId = (this.currentFractionId + 1) % this.fractions.length;

            return FRACTIONS[this.fractions[nextId].name];
        },

        getAboutFraction() {
            return this.aboutFractions[this.getCurrentFraction.name];
        },

        getActiveLeftCategory() {
            const category = this.categoriesName.find(c => this.selectedLeftCategory in c);
            return category[this.selectedLeftCategory];
        },

        getActiveRightCategory() {
            const category = this.categoriesName.find(c => this.selectedRightCategory in c);
            return category[this.selectedRightCategory];
        },

        getCardsInPool() {
            if (this.selectedLeftCategory === 'All')
                return this.cardsInPool;

            return this.cardsInPool.filter(c => (c.lines.includes(this.selectedLeftCategory) && (c.category === 'Hero' || c.category === 'None')) || c.category === this.selectedLeftCategory);
        },

        getCardsInDeck() {
            if (this.selectedRightCategory === 'All')
                return this.cardsInDeck;

            return this.cardsInDeck.filter(c => (c.lines.includes(this.selectedRightCategory) && (c.category === 'Hero' || c.category === 'None')) || c.category === this.selectedRightCategory);
        }
    },

    methods: {
        async gotoPrevFraction() {
            const error = await updateDeck(this.fractions[this.currentFractionId].name, this.cardsInDeck.map(c => c.id));
            
            if (error)
                this.showError(error);

            this.currentFractionId = (this.currentFractionId - 1 + this.fractions.length) % this.fractions.length;

            this.getDeck(this.fractions[this.currentFractionId].name);
        },

        async gotoNextFraction() {
            const error = await updateDeck(this.fractions[this.currentFractionId].name, this.cardsInDeck.map(c => c.id));

            if (error)
                this.showError(error);

            this.currentFractionId = (this.currentFractionId + 1) % this.fractions.length;

            this.getDeck(this.fractions[this.currentFractionId].name);
        },

        changeCategory(name, left = true) {
            const category = left ? 'selectedLeftCategory' : 'selectedRightCategory';

            if (this[category] === name)
                return;

            this[category] = name;
        },

        moveToDeck(card) {
            this.cardsInDeck.push(card);
            this.cardsInPool.splice(this.cardsInPool.indexOf(card), 1);
        },

        moveToPool(card) {
            this.cardsInPool.push(card);
            this.cardsInDeck.splice(this.cardsInDeck.indexOf(card), 1);
        },

        showError(msg) {
            this.error.text = msg;
        },

        async getDeck(fractionName) {
            const deck = await getDeckByFraction(fractionName);

            if (deck.error) {
                this.showError(deck.errorMessage);

                return;
            }

            const result = deck.result;

            this.cardsInPool = result.poolCards.cards;
            this.cardsInDeck = result.deckCards.cards;
        }
    }
}
</script>

<template>
<Notification :text="error.text" :onCloseClick="() => showError(undefined)" type="error" />

<section class="main-container">
    <header class="fractions-info">
        <div class="prev-fraction">
            <p>{{ getPreviousFraction }}</p>
            <div class="left-arrow" @click="gotoPrevFraction()" />
        </div>
        <div class="title">
            <img :src="getCurrentFraction.imagePath" />
            <div class="text">
                {{ getCurrentFractionName }}
                <ul class="fractions-queue">
                    <li v-for="(_, index) in fractions" :key="index" class="item"
                        :class="{ active: index === currentFractionId }">■</li>
                </ul>
            </div>
        </div>
        <div class="next-fraction">
            <div class="right-arrow" @click="gotoNextFraction()" />
            <p>{{ getNextFraction }}</p>
        </div>
    </header>

    <section class="additional-info">
        <div class="left-info">
            <h1>Card Collection</h1>
            <h2>{{ getActiveLeftCategory }}</h2>
        </div>

        <div class="about-fraction">{{ getAboutFraction }}</div>
        
        <div class="right-info">
            <h1>Cards in Deck</h1>
            <h2>{{ getActiveRightCategory }}</h2>
        </div>
    </section>

    <section class="main-section">
        <div class="types">
            <div class="left-group">
                <div :class="['item', 'card', { 'active': selectedLeftCategory === 'All' }]" @click="changeCategory('All')" />
                <div :class="['item', 'sword', { 'active': selectedLeftCategory === 'Closer' }]" @click="changeCategory('Closer')" />
                <div :class="['item', 'bow', { 'active': selectedLeftCategory === 'Ranger' }]" @click="changeCategory('Ranger')" />
                <div :class="['item', 'trebuchet', { 'active': selectedLeftCategory === 'Siege' }]" @click="changeCategory('Siege')" />
                <div :class="['item', 'head', { 'active': selectedLeftCategory === 'Hero' }]" @click="changeCategory('Hero')" />
                <div :class="['item', 'sun', { 'active': selectedLeftCategory === 'Weather' }]" @click="changeCategory('Weather')" />
                <div :class="['item', 'spikes', { 'active': selectedLeftCategory === 'Special' }]" @click="changeCategory('Special')" />
            </div>
            <div class="right-group">
                <div :class="['item', 'card', { 'active': selectedRightCategory === 'All' }]" @click="changeCategory('All', false)" />
                <div :class="['item', 'sword', { 'active': selectedRightCategory === 'Closer' }]" @click="changeCategory('Closer', false)" />
                <div :class="['item', 'bow', { 'active': selectedRightCategory === 'Ranger' }]" @click="changeCategory('Ranger', false)" />
                <div :class="['item', 'trebuchet', { 'active': selectedRightCategory === 'Siege' }]" @click="changeCategory('Siege', false)" />
                <div :class="['item', 'head', { 'active': selectedRightCategory === 'Hero' }]" @click="changeCategory('Hero', false)" />
                <div :class="['item', 'sun', { 'active': selectedRightCategory === 'Weather' }]" @click="changeCategory('Weather', false)" />
                <div :class="['item', 'spikes', { 'active': selectedRightCategory === 'Special' }]" @click="changeCategory('Special', false)" />
            </div>
        </div>

        <section class="cards">
            <section class="left-cards">
                <div class="frame">
                    <Card 
                        class="card"
                        v-for="card in getCardsInPool" 
                        @dblclick="moveToDeck(card)"
                        :cardId="card.id" 
                    />
                </div>
            </section>

            <section class="stats">
                <div class="leader">
                    <p>Leader</p>
                    <img :src=getCurrentFractionLeader>
                </div>
                <section class="info">
                    <div class="item">
                        <h1>Total cards in deck</h1>
                        <p>
                            <img src="../assets/decks/info/cards.svg" />
                            {{ stats.cardsInDeck }}
                        </p>
                    </div>
                    <div class="item">
                        <h1>Number of Unit Cards</h1>
                        <p style="color: #FF1C1C">
                            <img src="../assets/decks/info/swords.svg" />
                            {{ stats.unitCards }}/{{ maxStats.unitCards }}
                        </p>
                    </div>
                    <div class="item">
                        <h1>Special Cards</h1>
                        <p style="color: #30923C">
                            <img src="../assets/decks/info/green-card.svg" style="margin-right: 5px" />
                            {{ stats.specialCards }}/{{ maxStats.specialCards }}
                        </p>
                    </div>
                    <div class="item">
                        <h1>Total Unit Card Strength</h1>
                        <p>
                            <img src="../assets/decks/info/hand.svg" />
                            {{ stats.unitStrength }}
                        </p>
                    </div>
                    <div class="item">
                        <h1>Hero Cards</h1>
                        <p>
                            <img src="../assets/decks/info/head.svg" />
                            {{ stats.heroCards }}
                        </p>
                    </div>
                </section>
            </section>

            <section class="right-cards">
                <div class="frame">
                    <Card 
                        class="card"
                        v-for="card in getCardsInDeck" 
                        @dblclick="moveToPool(card)"
                        :cardId="card.id"
                    />
                </div>
            </section>
        </section>
    </section>
</section>
</template>

<style scoped lang="scss">
@use "../utilities/variables" as *;

* {
    color: $white;
}

.main-container {
    background-color: #060606;
    background-size: cover;
    background-repeat: no-repeat;
    height: calc(100vh - 90px);
}

.fractions-queue {
    display: flex;
    list-style-type: none;
    justify-content: center;
    gap: 4px;

    .item {
        color: rgb(115, 115, 115, 67%);
        font-size: 14px;
        user-select: none;
        transform: rotate(45deg);

        &.active {
            color: $bronze;
        }
    }
}

.additional-info {
    display: flex;
    justify-content: center;

    .about-fraction {
        color: $bronze;
        font-size: 23px;
        margin-top: 10px;
    }

    .right-info,
    .left-info {
        h1 {
            color: $gray;
            font-size: 28px;
        }

        h2 {
            color: #d6d3cc;
            font-size: 23px;
            min-width: 240px;
            filter: drop-shadow(0 0 6px $bronze-light);
            text-transform: uppercase;
        }
    }

    .left-info {
        margin-left: 60px;
        margin-right: auto;    
    }

    .right-info {
        margin-right: 60px;
        margin-left: auto;

        h1, 
        h2 {
            text-align: right;
        }
    }
}

.fractions-info {
    display: flex;
    justify-content: center;
    padding-top: 25px;
    margin-right: 60px;

    .prev-fraction,
    .next-fraction {
        display: flex;
        justify-content: center;
        align-items: center;

        p {
            color: #746D5D;
            font-size: 21px;
            margin-left: 25px;
            margin-right: 25px;
            width: 190px;
            text-align: right;
            user-select: none;
        }

        .right-arrow {
            width: 30px;
            height: 23px;
            background-image: url('../assets/decks/right-arrow.svg');
            margin-left: 70px;
            -webkit-transition: background-image 0.2s ease-in-out;
            transition: background-image 0.2s ease-in-out;

            &:hover {
                cursor: pointer;
                background-image: url('../assets/decks/right-arrow-hover.svg');
            }
        }

        .left-arrow {
            width: 30px;
            height: 23px;
            background-image: url('../assets/decks/left-arrow.svg');
            margin-right: 70px;
            -webkit-transition: background-image 0.2s ease-in-out;
            transition: background-image 0.2s ease-in-out;

            &:hover {
                cursor: pointer;
                background-image: url('../assets/decks/left-arrow-hover.svg');
            }
        }
    }
        
    .next-fraction p {
        text-align: left;
    }

    .title {
        display: flex;
        color: $white;
        font-size: 30px;
        min-width: 300px;
        justify-content: center;

        img {
            height: fit-content;
            width: 62px;
            margin-right: 18px;
        }

        .text {
            margin-top: 10px;
        }
    }
}

.main-section {
    display: flex;
    flex-direction: column;
    width: 100%;

    .cards {
        display: flex;
        height: 100%;

        .left-cards,
        .right-cards {
            display: flex;
            width: 50vw;
        }
    
        .left-cards {
            margin-left: 60px;
        }

        .right-cards {
            margin-right: 60px;
            justify-content: end;
        }
        
        .frame {
            display: grid;
            column-gap: 200px;
            row-gap: 8px;
            grid-template-columns: repeat(3, 0);
            grid-template-rows: repeat(7, auto);

            width: 625px;
            height: 59vh;
            padding: 15px;
            
            background-image: url('../assets/decks/frame.svg');
            background-repeat: no-repeat;
            background-size: contain;
            background-position: top;
            
            overflow-y: auto;
            scrollbar-width: none;
            -ms-overflow-style: none;

            &::-webkit-scrollbar {
                display: none;
            }

            .card {
                width: 192px;
                height: 354px;
            }
        }
    }

    .types {
        display: flex;
        margin-top: 20px;
        margin-bottom: 20px;

        .left-group,
        .right-group {
            display: flex;
            gap: 56px;
            width: 50vw;
        }

        .left-group {
            margin-left: 60px;
        }

        .right-group {
            margin-right: 60px;
            justify-content: end;
        }

        .item {
            width: 40px;
            height: 40px;

            background-repeat: no-repeat;
            background-size: contain;
            background-image: var(--background-image);

            transition: transform 0.1s ease-in-out;

            &:hover {
                cursor: pointer;
                transform: scale(1.15);
            }

            &.active {
                filter: drop-shadow(0 0 5px $bronze-light);
                background-image: var(--hover-background-image);
            }

            &.card {
                --background-image: url('../assets/decks/card.svg');
                --hover-background-image: url('../assets/decks/card-hover.svg');
            }

            &.bow {
                --background-image: url('../assets/decks/bow.svg');
                --hover-background-image: url('../assets/decks/bow-hover.svg');
            }

            &.sword {
                --background-image: url('../assets/decks/sword.svg');
                --hover-background-image: url('../assets/decks/sword-hover.svg');
            }

            &.head {
                --background-image: url('../assets/decks/head.svg');
                --hover-background-image: url('../assets/decks/head-hover.svg');
            }

            &.spikes {
                --background-image: url('../assets/decks/spikes.svg');
                --hover-background-image: url('../assets/decks/spikes-hover.svg');
            }

            &.trebuchet {
                --background-image: url('../assets/decks/trebuchet.svg');
                --hover-background-image: url('../assets/decks/trebuchet-hover.svg');
            }

            &.sun {
                --background-image: url('../assets/decks/sun.svg');
                --hover-background-image: url('../assets/decks/sun-hover.svg');
            }
        }
    }

    .stats {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 300px;

        .leader {
            width: 140px;
            margin-top: calc(-60px);

            p {
                font-size: 23px;
                color: $bronze;
                text-align: center;
            }

            img {
                margin-top: 15px;
                width: 140px;
            }
        }

        .info {
            display: flex;
            flex-direction: column;

            .item {
                gap: 6px;
                text-align: center;

                h1 {
                    color: $skiny;
                    font-size: 21px;
                }

                p {
                    margin-left: 30%;
                    display: flex;
                    color: $bronze;
                    font-size: 24px;

                    img {
                        margin-right: 12px;
                    }
                }
            }
        }
    }
}

@media screen and (max-height: 1280px) {
    .main-section .cards .frame {
        height: 60vh;
    }
}

@media screen and (max-height: 1025px) {
    .main-section { 
        .cards {
            .frame {
                height: 65vh;
                width: 540px;
                column-gap: 172px;
    
                .card {
                    width: 165px;
                    height: 305px;
                }
            }
        }

        .types {
            .left-group,
            .right-group {
                gap: 48px;
            }

            .item {
                width: 36px;
                height: 36px;
            }
        }
    }
}

@media screen and (max-height: 960px) {
    .main-section {
        .cards {
            .frame {
                height: 63vh;
                width: 490px;
                column-gap: 155px;
    
                .card {
                    width: 150px;
                    height: 277px;
                }
            }
        }

        .types {
            .left-group,
            .right-group {
                gap: 44px;
            }

            .item {
                width: 32px;
                height: 32px;
            }
        }
    }
}
</style>