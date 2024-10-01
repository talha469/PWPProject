// APIRequests.js
const BASE_URL = "https://localhost:7130";

const handleResponse = (response) => {
  if(!response?.ok){
    throw new Error(response?.status);
  }
  return response.json();
};

  
const api = {

    get: (endpoint,Id, queryParams) => {
      
      const token = localStorage.getItem('JWTToken');
        let url = `${BASE_URL}/${endpoint}`;

        if (queryParams) {
          url += `/${Id}`;
      }

        if (queryParams) {
            const queryString = new URLSearchParams(queryParams).toString();
            url += `?${queryString}`;
        }

        return fetch(url, {
              method: "GET",
              headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}` // Include the Authorization header with the JWT token
              }
            })
        .then(handleResponse)
        .catch((error) => {
          console.error("API error:", error);
          throw error;
        });
    },

    post: (endpoint, data) => {
      
      const token = localStorage.getItem('JWTToken');
 
            return fetch(`${BASE_URL}/${endpoint}`, {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}` // Include the Authorization header with the JWT token
              },
              body: JSON.stringify(data),
            })
              .then(handleResponse)
              .catch((error) => {
                console.error("API error:", error);
                throw error;
              });
        
          },

          put: (endpoint, data) => {
            
            const token = localStorage.getItem('JWTToken');
            
                  return fetch(`${BASE_URL}/${endpoint}`, {
                    method: "PUT",
                    headers: {
                      "Content-Type": "application/json",
                      "Authorization": `Bearer ${token}` // Include the Authorization header with the JWT token
                    },
                    body: JSON.stringify(data),
                  })
                    .then(handleResponse)
                    .catch((error) => {
                      console.error("API error:", error);
                      throw error;
                    });
              
                },

          postUser: (endpoint, data) => {
              return fetch(`${BASE_URL}/${endpoint}`, {
                method: "POST",
                headers: {
                  "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
              })
                .then(handleResponse)
                .catch((error) => {
                  console.error("API error:", error);
                  throw error;
                });
          
            },

            delete: (endpoint, Id) => {
              const token = localStorage.getItem('JWTToken');
                  
                  return fetch(`${BASE_URL}/${endpoint}/${Id}`, {
                      method: "DELETE",
                      headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${token}` // Include the Authorization header with the JWT token
                      },
                      body: JSON.stringify(Id),
                    })
                      .then(handleResponse)
                      .catch((error) => {
                        console.error("API error:", error);
                        throw error;
                      });
                
                  },
      
};

export default api;
