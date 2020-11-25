import { userService } from "../services/userservice";
import { router } from "../helpers";

const state = {
  all: {},
  user: null,
};

const actions = {
  async getAll({ commit }) {
    commit("getAllRequest");

    await userService.getAll().then(
      (users) => commit("getAllSuccess", users),
      (error) => commit("getAllFailure", error)
    );
  },

  async getById({ commit }, id) {
    commit("getByIdRequest");

    await userService.getById(id).then(
      (user) => commit("getByIdSuccess", user),
      (error) => commit("getByIdFailure", error)
    );
  },

  update({ commit }, user) {
    userService.update(user).then(
      (updateUser) => {
        commit("updateUserSuccess", updateUser);
        router.push("/inventory/home");
      },
      (error) => commit("updateUserFailure", error)
    );
  },

  delete({ commit }, id) {
    commit("deleteRequest", id);

    userService.delete(id).then(
      (user) => commit("deleteSuccess", id),
      (error) => commit("deleteFailure", { id, error: error.toString() })
    );
  },

  edit({ commit }, id) {
    router.push(`/inventory/editUser/${id}`);
  },
};

const mutations = {
  getAllRequest(state) {
    state.all = { loading: true };
  },
  getAllSuccess(state, users) {
    state.all = { items: users };
  },
  getAllFailure(state, error) {
    state.all = { error };
  },
  getByIdSuccess(state, user) {
    state.user = user;
  },
  getByIdRequest(state, user) {
    state.user = { loading: true };
  },
  getByIdFailure(state, error) {
    state.user = null;
  },
  updateUserSuccess(state, updateUser) {
    state.user = null;
  },
  updateUserFailure(state, error) {
    state.user = null;
  },
  deleteRequest(state, id) {
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
  },
};

const getters = {
  getUserDetails: (state) => {
    return state.user;
  },
};

export const users = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
