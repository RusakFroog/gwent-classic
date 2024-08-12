<script>
import Button from '../components/Button.vue';

export default {
    name: "DecksPage",
    components: {
        Button
    },

    methods: {
        getFractionImage() {
            return "src/assets/images/decks/fractions/" + this.currentFraction.name.toLowerCase() + ".png";
        },

        getFractionCardLeader() {
            return "src/assets/images/decks/leaders/" + this.currentFraction.leaderName.toLowerCase() + ".png";
        }
    },
    
    data() {
        return {
            currentFraction: {
                name: "Northern Realms",
                leaderName: "commander",
            },

            fractionsQueue: {

            },
        }
    }
}
</script>

<template>
    <section class="h-[calc(100vh-90px)] w-screen justify-start bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')] text-[#ffffff]">
        <header>
            <!-- FRACTIONS -->
            <div class="flex justify-center pt-4">
                <button>Nilfgaard <- </button>

                <!-- CURRENT FRACTION -->
                <div class="flex justify-center mx-8">
                    <img :src="getFractionImage()" class="w-[40px] mr-2" />
                    <h1 class="mt-1 text-2xl">{{ currentFraction.name }}</h1>
                </div>
                <!-- CURRENT FRACTION -->

                <button> -> Monters</button>
            </div>
            <!-- FRACTIONS -->

            <!-- QUEUE FRACTIONS -->
            <ul class="flex list-none justify-center gap-[7px]">
                <li class="fractions-item-queue" :class="{active: true}">■</li>
                <li class="fractions-item-queue" :class="{active: false}">■</li>
                <li class="fractions-item-queue" :class="{active: false}">■</li>
                <li class="fractions-item-queue" :class="{active: false}">■</li>
            </ul>
            <!-- QUEUE FRACTIONS -->

            <!-- DESCRIPTION CURRENT FRACTION -->
            <div class="flex justify-center pt-2">Scoiatel can take your card and delete from queue cards.</div>
            <!-- DESCRIPTION CURRENT FRACTION -->
        </header>

        <div class="flex gap-5 w-screen justify-center pt-2">
            <!-- COLLECTION CARDS -->
            <section class="w-[34vw] h-[50vh]">
                <div>Collection cards</div>
                <div class="my-2">SORT----------------</div>
                <div class="grid grid-cols-3 grid-rows-3 gap-4 bg-[#151515]">
                    <!-- <Card /> -->
                </div>
            </section>
            <!-- COLLECTION CARDS -->
            
            <section class="flex-row justify-center mx-32 mt-4">
                <!-- Leader -->
                <main>
                    <div class="text-center text-[#F1D177] mb-3">Leader</div>
                    <img :src="getFractionCardLeader()" class="w-[180px]" />
                </main>
                <!-- Leader -->
    
                <!-- Stats -->
                <section class="text-center mt-4">
                    <div>Cards in squad</div>
                    <div>3</div>
    
                    <div>Special cards</div>
                    <div>10/10</div>
    
                    <div>Total strength</div>
                    <div>112</div>
    
                    <div>Heroes</div>
                    <div>2</div>
                </section>
                <!-- Stats -->
            </section>

            <!-- DECK CARDS -->
            <section class="w-[34vw] h-[71vh]">
                <div class="text-end">Deck cards</div>
                <div class="my-2">SORT---------------------------</div>
                <ul class="grid-cards">
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                    <li>text1</li>
                </ul>
            </section>
            <!-- DECK CARDS -->
        </div>
    </section>
</template>

<style scoped>
* {
    font-family: "Witcher";
}

.fractions-item-queue {
    user-select: none;
    font-size: 10px;
    color: #383735;
    --tw-rotate: 45deg;
    transform: translate(var(--tw-translate-x), var(--tw-translate-y)) rotate(var(--tw-rotate)) skewX(var(--tw-skew-x)) skewY(var(--tw-skew-y)) scaleX(var(--tw-scale-x)) scaleY(var(--tw-scale-y));

    &.active {
        color: #D2B47C;
    }
}

::-webkit-scrollbar-track {
    background: #535353;
    border-radius: 1px;
}

::-webkit-scrollbar {
    width: 5px;
}

::-webkit-scrollbar-thumb {
    background: #be9999; 
}

::-webkit-scrollbar-thumb:hover {
    background: #7e6a6a; 
}

.grid-cards {
    display: grid;
    grid-template-columns: repeat(3, minmax(0, 1fr));
    grid-template-rows: repeat(3, minmax(0, 1fr));
    gap: 12px;
    overflow-y: auto;
    height: calc(71vh - 10%);
    padding-right: 10px;
    padding-left: 10px;
    padding-top: 5px;
    background-color: #151515;
}

.button-custom {
    width: 280px;
    height: 80px;
}
</style>