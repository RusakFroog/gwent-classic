<script>
import Button from '../components/ui/Button.vue';
import { FRACTIONS } from '../services/data/constants';

export default {
    name: "DecksPage",
    components: {
        Button
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
            fractions: [
                new Fraction("NorthernRealms", "commander"),
                new Fraction("Nilfgaardian", "emperor"),
                new Fraction("Monsters", "beast"),
                new Fraction("Skellige", "raider"),
                new Fraction("Scoiatael", "archer"),
            ],
            currentFractionId: 0,

            aboutFractions: { 
                "NorthernRealms": "Draw a card from your deck whenever you win a round.",
                "Nilfgaardian": "",
                "Monsters": "",
                "Skellige": "",
                "Scoiatael": "",
            }
        };
    },

    computed: {
        getCurrentFraction() {
            return this.fractions[this.currentFractionId];
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
        }
    },

    methods: {
        gotoPrevFraction() {
            this.currentFractionId = (this.currentFractionId - 1 + this.fractions.length) % this.fractions.length;
        },

        gotoNextFraction() {
            this.currentFractionId = (this.currentFractionId + 1) % this.fractions.length;
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
                        <li v-for="(_, index) in fractionsQueue" :key="index" class="item"
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
                <h2>ARCHERS CARDS</h2>
            </div>

            <div class="about-fraction">{{ getAboutFraction }}</div>
            <div class="right-info">
                <h1>Cards in Deck</h1>
                <h2>ARCHERS CARDS</h2>
            </div>
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
    width: 100vw;
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

        h1 {
            text-align: right;
        }
    }
}

.fractions-info {
    display: flex;
    justify-content: center;
    align-items: center;
    padding-top: 45px;
    
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

    .title {
        display: flex;
        color: $white;
        font-size: 30px;
        
        img {
            width: 60px;
            margin-right: 18px;
        }

        .text {
            margin-top: 10px;
        }
    }
}
</style>