<template>
  <v-app>
    <v-main>
      <!-- Your main content goes here -->
      <router-view></router-view>
    </v-main>

    <!-- Bottom navigation menu -->
    <v-bottom-navigation v-model="value" active color="primary">
      <v-btn>
        <v-icon>mdi-history</v-icon>

        Recents
      </v-btn>

      <v-btn>
        <v-icon>mdi-heart</v-icon>

        Favorites
      </v-btn>

      <v-btn>
        <v-icon>mdi-map-marker</v-icon>

        <span>Nearby</span>
      </v-btn>
    </v-bottom-navigation>
  </v-app>
</template>

<script>
export default {
  data: () => ({ value: 0 }),
};
</script>

<style>
/* Add your styles as needed */
</style>
