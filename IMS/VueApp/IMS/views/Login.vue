<template>

    <div class="columns is-mobile is-centered">
       <div class="column is-4">
      <h1 class="title">
        Login
      </h1>
       <form @submit.prevent="handleSubmit">
      <div class="field">
        <p class="control has-icons-left has-icons-right">
          <input class="input" type="email" placeholder="Email" v-model="email" name="email"/>
          <span class="icon is-small is-left">
            <i class="fas fa-envelope"></i>
          </span>
          <span class="icon is-small is-right">
            <i class="fas fa-check"></i>
          </span>
        </p>
        <div v-show="submitted && !email" class="invalid-feedback">email is required</div>
      </div>
      <div class="field">
        <p class="control has-icons-left">
          <input class="input" type="password" placeholder="Password" v-model="password" name="password"/>
          <span class="icon is-small is-left">
            <i class="fas fa-lock"></i>
          </span>
        </p>
         <div v-show="submitted && !password" class="invalid-feedback">Password is required</div>
      </div>
      <div class="field">
        <p class="control">
          <button type="submit" class="button is-success">
            Login
          </button>
        </p>
      </div>
       </form>
    </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    data () {
        return {
            email: '',
            password: '',
            submitted: false
        }
    },
    computed: {
        ...mapState('account', ['status'])
    },
    created () {
        // reset login status
        this.logout();
    },
    methods: {
        ...mapActions('account', ['login', 'logout']),
        handleSubmit (e) {
            this.submitted = true;
            const { email, password } = this;
            if (email && password) {
                this.login({ email, password })
            }
        }
    }
};
</script>

<style scoped></style>
