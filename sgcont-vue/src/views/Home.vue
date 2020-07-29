<template>
  <v-container>
    <h2>Cuadro de mando</h2>
    <v-layout row wrap>
      <v-row>
        <v-col cols="12">
          <Ofertas></Ofertas>
        </v-col>
        <v-col cols="12">
          <ContratosProxVen></ContratosProxVen>
        </v-col>
      </v-row>
    </v-layout>
  </v-container>
</template>

<script>
import api from "@/api";
import Ofertas from "@/components/dashboard/SGCont/Ofertas.vue";
import OfertasTotal from "@/components/dashboard/SGCont/OfertasTotal.vue";
import ContratosProxVen from "@/components/dashboard/SGCont/ContratosProxVen.vue";
import ContratosTipo from "@/components/dashboard/SGCont/ContratosTipo.vue";

export default {
  components: {
    Ofertas,
    OfertasTotal,
    ContratosProxVen,
    ContratosTipo
  }
};
</script>
