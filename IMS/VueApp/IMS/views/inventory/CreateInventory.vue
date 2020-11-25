<template>
    <div class="columns is-mobile is-centered">
       <div class="column is-4">
      <h1 class="title">
        Create Inventory Item
      </h1>
       <form @submit.prevent="handleSubmit">
       <div class="field">
        <p class="control has-icons-left has-icons-right">
          <input class="input" type="text" placeholder="Item Name" v-model="inventory.inventoryName" name="inventoryName"/>
        </p>
        <div v-show="submitted && !inventory.inventoryName" class="invalid-feedback">inventory Name is required</div>
      </div>

       <div class="field">
        <p class="control has-icons-left has-icons-right">
          <input class="input" type="number" placeholder="Quantity" v-model="inventory.quantity" name="Quantity"/>
        </p>
        <div v-show="submitted && !inventory.quantity" class="invalid-feedback">Quantity is required</div>
      </div>

      <div class="field">
            <label class="label">Select User</label>
            <div class="control">
                <div class="select">
                <select  name="user" v-model="inventory.userId">
                      <option  v-for="user in users.items" :key="user.id" v-bind:value="user.id">{{user.firstName}}</option>
                </select>
                </div>
            </div>
            <div v-show="submitted && !inventory.userId" class="invalid-feedback">User is required</div>
        </div>

      <div class="field">
        <p class="control">
          <button type="submit" class="button is-success">
            Create
          </button>
        </p>
      </div>
       </form>
        <router-link :to="{ name: 'createInventory' }" class="btn btn-link">Cancel</router-link>
    </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    data () {
        return {
            inventory: {
                inventoryName: '',
                quantity: 0,
                userId:0,
            },
            submitted: false
        }
    },
    computed: {
        ...mapState({
            users: state => state.users.all
        })
    },
    created () {
        this.getAllUsers();
    },
     methods: {
        ...mapActions('users', {getAllUsers: 'getAll',}),
        ...mapActions('inventories', ['createInventoryItem']),
         
         handleSubmit(e) {
            this.submitted = true;
            const { inventoryName, quantity, userId } = this.inventory;

            console.log(this.inventory);
            
            if (inventoryName && quantity && userId) {
                let temp = {userId: this.inventory.userId, inventoryName: this.inventory.inventoryName, quantity: parseInt(this.inventory.quantity)}
                 this.createInventoryItem(temp);
                 this.inventory = {
                      inventoryName: '',
                      quantity: 0,
                      userId:0,
                  }
            }
             
        }
    },
};
</script>