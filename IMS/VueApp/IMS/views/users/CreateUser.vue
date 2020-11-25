<template>
    <div class="columns is-mobile is-centered">
       <div class="column is-4">
      <h1 class="title">
        Create User
      </h1>
       <form @submit.prevent="handleSubmit">
       <div class="field">
        <p class="control has-icons-left has-icons-right">
          <input class="input" type="text" placeholder="First Name" v-model="user.firstName" name="firstName"/>
        </p>
        <div v-show="submitted && !user.firstName" class="invalid-feedback">First Name is required</div>
      </div>

      <div class="field">
        <p class="control has-icons-left has-icons-right">
          <input class="input" type="text" placeholder="Last Name" v-model="user.lastName" name="lastName"/>
        </p>
        <div v-show="submitted && !user.lastName" class="invalid-feedback">Last Name is required</div>
      </div>


      <div class="field">
        <p class="control has-icons-left has-icons-right">
          <input class="input" type="email" placeholder="Email" v-model="user.email" name="email"/>
          <span class="icon is-small is-left">
            <i class="fas fa-envelope"></i>
          </span>
          <span class="icon is-small is-right">
            <i class="fas fa-check"></i>
          </span>
        </p>
        <div v-show="submitted && !user.email" class="invalid-feedback">email is required</div>
      </div>

      <div class="field">
        <p class="control has-icons-left">
          <input class="input" type="password" placeholder="Password" v-model="user.password" name="password"/>
          <span class="icon is-small is-left">
            <i class="fas fa-lock"></i>
          </span>
        </p>
         <div v-show="submitted && !user.password" class="invalid-feedback">Password is required</div>
      </div>

        <div class="field">
            <label class="label">Role</label>
            <div class="control">
                <div class="select">
                <select v-model="user.role" name="role">
                    <option disabled value="">Select Role</option>
                    <option>Admin</option>
                    <option>Manager</option>
                    <option>Viewer</option>
                </select>
                </div>
            </div>
            <div v-show="submitted && !user.role" class="invalid-feedback">Role is required</div>
        </div>

         <div class="field">
        <label class="label">Is Enabled</label>
        <div class="control">
            <label class="radio">
                <input type="radio" v-bind:value="true" v-model="user.isEnabled" checked>
                true
            </label>
            <label class="radio">
                <input type="radio" v-bind:value="false" v-model="user.isEnabled">
                false
            </label>
        </div>
         </div>


      <div class="field">
        <p class="control">
          <button type="submit" class="button is-success">
            Create User
          </button>
        </p>
      </div>
       </form>
        <router-link :to="{ name: 'home' }" class="btn btn-link">Cancel</router-link>
    </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    data () {
        return {
            user: {
                firstName: '',
                lastName: '',
                email: '',
                password: '',
                role:'',
                isEnabled:true,
            },
            submitted: false
        }
    },
    computed: {
        ...mapState('account', ['status'])
    },
    methods: {
        ...mapActions('account', ['register']),
        handleSubmit(e) {
            this.submitted = true;
            const { email, password, firstName,lastName,role } = this.user;
            
            if (email && password && firstName && lastName && role) {
                 this.register(this.user);
            }
             
        }
    }
};
</script>