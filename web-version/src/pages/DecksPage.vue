<script>
import Button from '../components/ui/Button.vue';
import Card from '../components/decks/Card.vue';
import { FRACTIONS } from '../services/data/constants';

export default {
    name: "DecksPage",
    components: {
        Button,
        Card
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
                "ALL CARDS",
                "CLOSE COMBAT UNIT CARDS",
                "RANGED UNIT CARDS",
                "SIEGE UNIT CARDS",
                "HERO CARDS",
                "WEATHER CARDS",
                "SPECIAL CARDS",
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

            selectedRightCategoryId: 0,
            selectedLeftCategoryId: 0,

            cardsInDeck: [
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
                {
                    id: 0,
                    subCategory: 0,
                },
            ],
            cardsInPool: [
                {
                    id: 0,
                    cardCategory: 0,
                } 
            ],
        };
    },

    mounted() {
        console.log(window.navigator.userAgent);

        // get cards in deck etc.
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
            return this.categoriesName[this.selectedLeftCategoryId];
        },

        getActiveRightCategory() {
            return this.categoriesName[this.selectedRightCategoryId];
        },

        getCardsInPool() {
            if (this.selectedLeftCategoryId === 0)
                return this.cardsInPool;

            return this.cardsInPool.filter(c => c.cardCategory === this.selectedLeftCategoryId);
        },

        getCardsInDeck() {
            if (this.selectedRightCategoryId === 0)
                return this.cardsInDeck;

            return this.cardsInDeck.filter(c => c.cardCategory === this.selectedRightCategoryId);
        }
    },

    methods: {
        gotoPrevFraction() {
            this.currentFractionId = (this.currentFractionId - 1 + this.fractions.length) % this.fractions.length;
        },

        gotoNextFraction() {
            this.currentFractionId = (this.currentFractionId + 1) % this.fractions.length;
        },

        changeCategory(id, left = true) {
            const categoryId = left ? 'selectedLeftCategoryId' : 'selectedRightCategoryId';

            if (this[categoryId] === id)
                return;

            this[categoryId] = id;
        }
    }
}
</script>

<template>
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
                <div :class="['item', 'card', { 'active': selectedLeftCategoryId === 0 }]" @click="changeCategory(0)" />
                <div :class="['item', 'sword', { 'active': selectedLeftCategoryId === 1 }]" @click="changeCategory(1)" />
                <div :class="['item', 'bow', { 'active': selectedLeftCategoryId === 2 }]" @click="changeCategory(2)" />
                <div :class="['item', 'trebuchet', { 'active': selectedLeftCategoryId === 3 }]" @click="changeCategory(3)" />
                <div :class="['item', 'head', { 'active': selectedLeftCategoryId === 4 }]" @click="changeCategory(4)" />
                <div :class="['item', 'sun', { 'active': selectedLeftCategoryId === 5 }]" @click="changeCategory(5)" />
                <div :class="['item', 'spikes', { 'active': selectedLeftCategoryId === 6 }]" @click="changeCategory(6)" />
            </div>
            <div class="right-group">
                <div :class="['item', 'card', { 'active': selectedRightCategoryId === 0 }]" @click="changeCategory(0, false)" />
                <div :class="['item', 'sword', { 'active': selectedRightCategoryId === 1 }]" @click="changeCategory(1, false)" />
                <div :class="['item', 'bow', { 'active': selectedRightCategoryId === 2 }]" @click="changeCategory(2, false)" />
                <div :class="['item', 'trebuchet', { 'active': selectedRightCategoryId === 3 }]" @click="changeCategory(3, false)" />
                <div :class="['item', 'head', { 'active': selectedRightCategoryId === 4 }]" @click="changeCategory(4, false)" />
                <div :class="['item', 'sun', { 'active': selectedRightCategoryId === 5 }]" @click="changeCategory(5, false)" />
                <div :class="['item', 'spikes', { 'active': selectedRightCategoryId === 6 }]" @click="changeCategory(6, false)" />
            </div>
        </div>

        <section class="cards">
            <section class="left-cards">
                <div class="frame">
                    <Card 
                        class="card"
                        v-for="card in getCardsInPool" 
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
                        :cardId="card.id" 
                    />
                </div>
            </section>
        </section>
    </section>
</section>
</template>

<style scoped lang="scss">
@import "../utilities/variables.scss";

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

        .left-cards,
        .right-cards {
            display: flex;
            width: 50vw;
            height: 65vh;
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
            column-gap: 212px;
            row-gap: 15px;
            grid-template-columns: repeat(3, 0);
            grid-template-rows: repeat(7, auto);

            width: 648px;
            height: 64vh;
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
        }
    }

    .types {
        display: flex;
        margin-top: 20px;
        margin-bottom: 20px;

        .left-group {
            display: flex;
            gap: 60px;
            
            margin-left: 60px;
            width: 50vw;
        }

        .right-group {
            display: flex;
            gap: 60px;

            margin-right: 60px;
            justify-content: end;
            width: 50vw;
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
</style>