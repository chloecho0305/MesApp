<template>
  <div class="todo-app">
    <h1>Todolist</h1>

     <form @submit.prevent="addTodo">
       <input v-model="newTodo" placeholder="輸入待辦事項">
       <button type="submit">新增</button>
      </form>
    <ul>
      <li v-for="todo in todos" :key="todo.id">
        <input type="checkbox" v-model="todo.isCompleted" @change="updateTodo(todo)" />
        <span :style="{ textDecoration: todo.isCompleted ? 'line-through' : 'none' }">
          {{ todo.title }}
        </span>
        <button @click="deleteTodo(todo)">X</button>
      </li>
      </ul>
    
  </div>
  </template>
<script setup>
  import { ref, onMounted } from 'vue'
  import axios from 'axios'
  const todos = ref([])
  const newTodo = ref('')
  const fetchTodos = async () => {
    const response = await axios.get('/api/todos')
    console.log("伺服器回傳 todos：", response.data)
    todos.value = response.data
  }
  const addTodo = async () => {
    if (!newTodo.value.trim()) return
    const response = await axios.post('/api/todos', {
      title: newTodo.value,
      isCompleted: false
    })
    todos.value.push(response.data)
    newTodo.value = ''
  }
  const updateTodo = async (todo) => {
    await axios.put(`/api/todos/${todo.id}`, todo)
  }
  const deleteTodo = async (todo) => {
    await axios.delete(`/api/todos/${todo.id}`)
    todos.value = todos.value.filter(t => t.id !== todo.id)
  }

  onMounted(() => {
    fetchTodos()
  })
</script>
<style>
  .todo-app {
    max-width: 600px;
    margin: 0 auto;
    padding: 20px;
    font-family: Arial, sans-serif;
  }
  input[type="text"] {
    padding:0.5rem;
    width:60%;
  }
  button {
    margin-left:0.5rem;
  }

</style>
