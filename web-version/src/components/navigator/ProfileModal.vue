<script>
import Button from '../ui/Button.vue'; 
import CustomInput from '../ui/CustomInput.vue';
import { logoutAccount, updateAccount } from '../../services/accounts.js';

export default {
    name: "ProfileModal",
    components: {
        Button,
        CustomInput
    },

    data() {
        return {
            image: 'https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/ab728018-e315-44d3-88f3-12a0045cc5f3/d8p1wqo-3b4d78e8-db49-4a66-99c1-882a64c82be0.jpg/v1/fit/w_828,h_1148,q_70,strp/geralt_portrait_by_yamaorce_d8p1wqo-414w-2x.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9MTIwMCIsInBhdGgiOiJcL2ZcL2FiNzI4MDE4LWUzMTUtNDRkMy04OGYzLTEyYTAwNDVjYzVmM1wvZDhwMXdxby0zYjRkNzhlOC1kYjQ5LTRhNjYtOTljMS04ODJhNjRjODJiZTAuanBnIiwid2lkdGgiOiI8PTg2NSJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS5vcGVyYXRpb25zIl19.W1GcpQf9qlWAP_ivpHfJtdewYa4v9pndidCNdypYP80',
            inputShow: false,
            nickName: "",

            inputs: {
                inputNickName: null
            }
        }
    },

    methods: {
        async editProfile() {
            // When input is will be hidden
            if (this.inputShow == true) {
                const nickName = this.inputs.inputNickName.inputValue;
                
                if (this.nickName == nickName) {
                    this.inputShow = !this.inputShow;
                    return;
                }

                if (!nickName.match(/^[a-zA-Z0-9_]+$/))
                    return alert("Nickname can only contain 'a-Z', '0-9' and '_'");
                
                const updateProfile = await this.updateProfile(nickName);

                if (!updateProfile)
                    return alert(updateProfile.error);
                
                this.nickName = nickName;
            }
            
            this.inputShow = !this.inputShow;
        },

        async updateProfile(nickName) {
            const result = await updateAccount(nickName);

            if (!result.response.ok)
                return {error: result.error};

            return true;
        },

        async logout() {
            await logoutAccount();

            sessionStorage.setItem('new_session', 'true');
            
            this.$router.push('/login');
        }
    },

    beforeMount() {
        const cookies = document.cookie.split(";");
        const nickName = cookies.find(x => x.includes("account_nickname")).split("=")[1];

        this.nickName = nickName;
    },

    mounted() {
        this.inputs.inputNickName = this.$refs['nick_name_input'];
    }
}
</script>

<template>
    <section class="profile-container">
        <h3 class="profile-title">PROFILE</h3>
        <section class="profile-content">
            <div class="profile-header">
                <div class="profile-image-container">
                    <img class="profile-image" :src="image">
                </div>
            </div>
            <!-- NICKNAME -->
            <h1 v-show="!inputShow" :style="nickName.length > 15 ? 'font-size: 20px;' : 'font-size: 24px;'" class="nickname">{{ nickName }}</h1>
            <CustomInput v-show="inputShow" class="custom-input" placeholder="NICKNAME" type="text" ref="nick_name_input" :valueInput="nickName" />
            <!-- NICKNAME -->
            <Button class="button" @click="editProfile()" text="EDIT PROFILE" />
            <Button class="button" @click="logout()" text="LOG OUT" />
        </section>
    </section>
</template>

<style scoped lang="scss">
@import "../../utilities/variables.scss";

.profile-container {
    display: flex;
    position: absolute;
    right: 0;
    width: 310px;
    height: calc(100vh - 90px);
    align-items: center;
    flex-direction: column;
    background-color: rgba(13, 13, 13, 0.65);
}

.profile-title {
    margin-top: 20px;
    color: $bronze;
    font-size: 4.5rem;
    font-family: 'Witcher Alternative';
}

.profile-content {
    margin-top: 50px;
    width: 75%;
    height: 400px;
    background-color: $black-light;
}

.profile-header {
    display: flex;
    align-items: center;
    flex-direction: column;
    background-color: #302D2D;
    height: 128px;
}

.profile-image-container {
    margin-top: 44px;
}

.profile-image {
    border-radius: 50%;
    object-fit: cover;
    width: 140px;
    height: 140px;
    outline: 8px solid $black-light;
}

.nickname {
    margin-top: 70px;
    color: $white;
    text-align: center;
    overflow: hidden;
    text-overflow: ellipsis;
}

.custom-input {
    width: 208px;
    height: 50px;
    font-size: 24px;
    margin-left: 12px;
    margin-top: 65px;
    padding: 5px 20px;
}

.button {
    width: 208px;
    height: 60px;
    font-size: 24px;
    margin: 35px auto 5px;
}
</style>