import { inventoryService } from "../services/inventorservice";
import { mapState, mapActions } from "vuex";

const state = {
  inventories: {},
};

const actions = {
  getAll({ commit }, id: Number) {
    commit("getAllInventoriesById");

    inventoryService.getInventoriesByUserId(id).then(
      (inventoryList) => commit("getAllSuccess", inventoryList),
      (error) => commit("getAllFailure", error)
    );
  },

  /*delete({ commit }, id) {
      commit("deleteRequest", id);
  
      userService.delete(id).then(
        (user) => commit("deleteSuccess", id),
        (error) => commit("deleteFailure", { id, error: error.toString() })
      );
    },*/
};

const mutations = {
  getAllInventoriesById(state) {
    state.inventories = { loading: true };
  },
  getAllSuccess(state, inventoryList) {
    state.inventories = { items: inventoryList };
  },
  getAllFailure(state, error) {
    state.inventories = { error };
  },
  /*deleteRequest(state, id) {
      state.all.items = state.all.items.map((user) =>
        user.id === id ? { ...user, deleting: true } : user
      );
    },
    deleteSuccess(state, id) {
      state.all.items = state.all.items.filter((user) => user.id !== id);
    },
    deleteFailure(state, { id, error }) {
      state.all.items = state.items.map((user) => {
        if (user.id === id) {
          const { deleting, ...userCopy } = user;
          return { ...userCopy, deleteError: error };
        }
  
        return user;
      });
    },*/
};

export const inventories = {
  namespaced: true,
  state,
  actions,
  mutations,
};
