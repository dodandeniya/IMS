<template>
    <div>
<nav class="navbar is-white shadow">
    <div class="container">
        <div class="navbar-brand">
            <a class="navbar-item brand-text"
               href="../">
                IMS
            </a>
            <div class="navbar-burger burger"
                 data-target="navMenu">
                <span></span>
                <span></span>
                <span></span>
            </div>
        </div>
        <div id="navMenu"
             class="navbar-menu">
            <div class="navbar-end">
                <div v-if="this.$route.path !== '/inventory/login'" class="navbar-item">
                    Hi {{account.user.firstName}}!
                </div>
               <div v-if="this.$route.path !== '/inventory/login'"  class="navbar-item"> 
                   <router-link :to="{ name: 'login' }">Logout</router-link>
               </div>
            </div>

        </div>
    </div>
</nav>
        <div class="container">
            <div class="column is-12">
                    <section class="section">
                        <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
                        <router-view></router-view>
                    </section>
            </div>
        </div>

        <div class="container">
            <footer>
                <hr />
                <p>&copy; 2020 - IMS</p>
            </footer>
        </div>

    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import Notifications from 'vue-notification'

Vue.use(Notifications)

export default {
    name: 'app',
    computed: {
        ...mapState({
            alert: state => state.alert,
             account: state => state.account
        })
    },
    methods: {
        ...mapActions({
            clearAlert: 'alert/clear' 
        })
    },
    watch: {
        $route (to, from){
            this.clearAlert();
        }
    } 
};
</script>

<style scoped lang="scss">
</style>
