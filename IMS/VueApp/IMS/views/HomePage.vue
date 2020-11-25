<template>
    <div>
        <router-link :to="{ name: 'create' }" class="button is-link" style="margin:10px;" v-if="account.user.role=='Admin'">Create User</router-link>
        <router-link :to="{ name: 'createInventory' }" class="button is-link" style="margin:10px;">create Inventory</router-link>
        <h1 class="title">Available Users</h1>
        <em v-if="users.loading">Loading users...</em>
        <span v-if="users.error" class="has-text-danger">ERROR: {{users.error}}</span>

         <table v-if="users.items" class="table is-fullwidth">
            <thead>
                <tr>
                    <th>User Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Role</th>
                    <th>Status</th>
                    <th v-if="account.user.role=='Admin'"></th>
                    <th v-if="account.user.role=='Admin'"></th>
                </tr>
            </thead>
            <tbody >
                <tr  v-for="user in users.items" :key="user.id">
                    <td>{{user.id}}</td>
                    <td>{{user.firstName}}</td>
                    <td>{{user.lastName}}</td>
                    <td>{{user.email}}</td>
                    <td>{{user.role}}</td>
                    <td>
                        <span v-if="user.isEnabled" style="color:green;">Enabled</span>
                        <span v-else style="color:#c2c2c2;">Disabled</span>
                    </td>
                    <td v-if="account.user.role=='Admin'">
                        <a @click="editUser(user.id)" class="button is-info">Edit</a>
                    </td>
                    <td v-if="account.user.role=='Admin'"> 
                        <span v-if="user.deleting"><em>Deleting...</em></span>
                        <span v-else-if="user.deleteError" class="has-text-danger">ERROR: {{user.deleteError}}</span>
                        <span v-else><a @click="deleteUser(user.id)" class="button is-danger">Delete</a></span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import { router } from "../helpers";

export default {
    computed: {
        ...mapState({
            users: state => state.users.all,
            account: state => state.account,
        })
    },
    created () {
        this.getAllUsers();
    },
    methods: {
        ...mapActions('users', {
            getAllUsers: 'getAll',
            deleteUser: 'delete',
            editUser: 'edit'
        }),
    },
};
</script>