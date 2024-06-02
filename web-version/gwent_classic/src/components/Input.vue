<script>
export default {
  props: {
    placeholder: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      active: false,
      inputValue: ''
    };
  },
  methods: {
    toggle() {
      this.active = true;
    },
    untoggle() {
      this.active = false;
    }
  }
};
</script>

<template>
  <div class="custom-input" :class="{ 'active': active }">
    <input :placeholder="placeholder" class="input-field text-[#8D8D8D] font-witcher"
           v-bind:value="inputValue" @input="updateInputValue($event.target.value)"
           @focus="toggle()" @blur="untoggle()" />
  </div>
</template>

<style scoped>
.custom-input {
  position: relative;
  display: inline-block;
}

.input-field {
  width: 403px;
  height: 88px;
  padding-right: 30px;
  background: url(../assets/images/registration/input.svg) no-repeat;
  background-size: cover;
  outline: none;
  font-size: 40px;
  text-align: left;
  padding-left: 30px;
}

.custom-input.active .input-field {
  background-image: url(../assets/images/registration/selected-input.svg);
}
</style>
