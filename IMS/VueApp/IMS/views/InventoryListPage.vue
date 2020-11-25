<template>
    <div>    
        <h1 class="title">Inventory List</h1>

        <em v-if="inventories.loading">Loading list...</em>
        <span v-if="inventories.error" class="has-text-danger">ERROR: {{inventories.error}}</span>

        <table v-if="inventories.items" class="table is-fullwidth">
            <thead>
                <tr>
                    <th>Item Id</th>
                    <th>Item Name</th>
                    <th>Quantity</th>
                </tr>
            </thead>
            <tbody >
                <tr v-for="item in inventories.items" :key="item.id">
                    <td>{{item.id}}</td>
                    <td>{{item.inventoryName}}</td>
                    <td>{{item.quantity}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    computed: {
        ...mapState({
            account: state => state.account,
            inventories: state => state.inventories.inventories
        })
    },
    created () {
        this.getAllInventories(this.account.user.id);
    },
    methods: {
        ...mapActions('inventories', {
            getAllInventories: 'getAll'
        })
    }
}
</script>